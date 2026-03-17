using JobAgent.API.Models;

namespace JobAgent.API.Services.Interfaces
{
    public interface IResumeService
    {
        Task<Resume> UploadResumeAsync(IFormFile file);
    }
}
