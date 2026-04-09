using System.Collections.Generic;
using System.Threading.Tasks;
using JobAgent.API.Models;

namespace JobAgent.API.Repositories.Interfaces
{
    public interface IJobRepository
    {   
        Task<List<Job>> GetAllJobsAsync();

        Task AddJobAsync(Job job);
    }
}
