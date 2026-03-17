using JobAgent.API.Models;

namespace JobAgent.API.Repositories.Interfaces
{
    public interface IResumeRepository
    {
        Task<Resume> SaveAsync(Resume resume);
    }
}
