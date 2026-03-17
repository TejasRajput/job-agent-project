using JobAgent.API.Models;

namespace JobAgent.API.Services
{
    public interface IJobService
    {
        List<Job> MatchJobs(List<string> candidateSkills);
    }
}
