using Microsoft.EntityFrameworkCore;
using JobAgent.API.Data;
using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;

namespace JobAgent.API.Repositories.Implementations
{
    public class JobRepository: IJobRepository
    {
      private readonly ApplicationDbContext _db;

        public JobRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _db.Jobs.ToListAsync();
        }

        public async Task AddJobAsync(Job job)
        {
            _db.Jobs.Add(job);
            await _db.SaveChangesAsync();
        }
    }
}
