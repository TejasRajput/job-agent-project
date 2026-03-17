using JobAgent.API.Models;

namespace JobAgent.API.Repositories.Interfaces
{
    public interface IJobRepository
    {
        List<Job> GetAllJobs();
    }
}
