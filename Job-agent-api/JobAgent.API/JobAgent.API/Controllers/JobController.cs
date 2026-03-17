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
            // 1️⃣ Call Python AI to parse resume
            var aiResult = await _aiService.ParseResume(resumeDto.FilePath);

            // 2️⃣ Deserialize Python result
            var parsedSkills = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(aiResult);
            var skills = parsedSkills?["skills"] ?? new List<string>();

            // 3️⃣ Match jobs
            var matchedJobs = _jobService.MatchJobs(skills);

            // 4️⃣ Map to DTO for frontend
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
