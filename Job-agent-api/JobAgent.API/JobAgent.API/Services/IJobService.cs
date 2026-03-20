using JobAgent.API.Models;

namespace JobAgent.API.Services
{
    public interface IJobService
    {
        List<Job> MatchJobs(List<string> skills, int experience, List<string> education);
    }
}
