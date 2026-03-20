using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;
using JobAgent.API.Services.Interfaces;

namespace JobAgent.API.Services.Implementations
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _repository;
        private readonly IAiService _aiService;

        public ResumeService(IResumeRepository repository, IAiService aiService)
        {
            _repository = repository;
            _aiService = aiService;
        }


        public async Task<Resume> UploadResumeAsync(IFormFile file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes", file.FileName);

            Directory.CreateDirectory("Resumes");

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var resume = new Resume
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                FilePath = filePath,
                UploadedAt = DateTime.UtcNow
            };

            await _repository.SaveAsync(resume);

            await _aiService.ParseResume(filePath);

            return resume;
        }
    }
}
