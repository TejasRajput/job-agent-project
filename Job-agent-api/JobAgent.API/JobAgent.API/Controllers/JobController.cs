using JobAgent.API.DTOs;
using JobAgent.API.Services;
using JobAgent.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Text.Json;

namespace JobAgent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobService _jobService;
        private readonly IAiService _aiService;

        public JobController(IJobService jobService, IAiService aiService)
        {
            _jobService = jobService;
            _aiService = aiService;
        }


        [HttpPost("recommend")]
        public async Task<IActionResult> RecommendJobs([FromBody] ResumeDto resumeDto)
        {
            var aiResult = await _aiService.ParseResume(resumeDto.FilePath);

            var parsed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(aiResult);

            // Skills
            var skills = parsed != null && parsed.TryGetValue("skills", out var skillsElem)
                ? JsonSerializer.Deserialize<List<string>>(skillsElem.GetRawText()) ?? new List<string>()
                : new List<string>();

            // Experience
            int experience = 0;
            if (parsed != null && parsed.TryGetValue("experience", out var expElem))
            {
                if (expElem.ValueKind == JsonValueKind.Number && expElem.TryGetInt32(out var expVal))
                    experience = expVal;
                else if (expElem.ValueKind == JsonValueKind.String)
                {
                    var expString = expElem.GetString() ?? "0";
                    var digits = new string(expString.Where(char.IsDigit).ToArray());
                    int.TryParse(digits, out experience);
                }
            }

            // Education
            var education = parsed != null && parsed.TryGetValue("education", out var eduElem)
                ? JsonSerializer.Deserialize<List<string>>(eduElem.GetRawText()) ?? new List<string>()
                : new List<string>();

            var matchedJobs = _jobService.MatchJobs(skills, experience, education);

            var jobDtos = matchedJobs.Select(job => new JobDto
            {
                Title = job.Title,
                Company = job.Company,
                Location = job.Location,
                RequiredSkills = job.RequiredSkills
            }).ToList();

            return Ok(jobDtos);
        }
    }
}
