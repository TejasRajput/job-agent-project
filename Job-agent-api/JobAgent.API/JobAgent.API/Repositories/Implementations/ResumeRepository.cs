using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;


namespace JobAgent.API.Repositories.Implementations
{
    public class ResumeRepository : IResumeRepository
    {
        private static readonly List<Resume> _db = new();

        public Task<Resume> SaveAsync(Resume resume)
        {
            _db.Add(resume);
            return Task.FromResult(resume);
        } 
    }
}
