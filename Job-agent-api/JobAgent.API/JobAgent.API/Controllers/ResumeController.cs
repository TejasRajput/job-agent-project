using JobAgent.API.Services.Implementations;
using JobAgent.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobAgent.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        private readonly IAiService _aiService;

        public ResumeController(IResumeService resumeService, IAiService aiService )
        {
            _resumeService = resumeService;
            _aiService = aiService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _resumeService.UploadResumeAsync(file);

            return Ok(result);
        }

        [HttpPost("parse")]
        public async Task<IActionResult> ParseResume()
        {
            var result = await _aiService.ParseResume("Resumes/tejas_resume.pdf");
            return Ok(result);
        }
    }
}
