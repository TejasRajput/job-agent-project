using JobAgent.API.Models;

namespace JobAgent.API.Services.Interfaces
{
    public interface IJobMatchingService
    {
        Task<List<JobMatchResult>> GetMatchedJobsAsync (string resumeText);
    }
}
