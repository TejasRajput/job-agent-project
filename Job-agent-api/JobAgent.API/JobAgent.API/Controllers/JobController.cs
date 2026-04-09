using JobAgent.API.DTOs;
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

            var matchedJobs = await _jobService.MatchJobsSemantic(resumeDto.FilePath);

            var jobDtos = matchedJobs.Select(job => new JobDto
            {
                Title = job.Title,
                Company = job.Company,
                Location = job.Location,
                RequiredSkills = job.RequiredSkills
            }).ToList();

            return Ok(jobDtos);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJob([FromBody] JobDto request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            var job = await _jobService.CreateJobAsync(request);

            return Ok(job);
        }
    }
}
