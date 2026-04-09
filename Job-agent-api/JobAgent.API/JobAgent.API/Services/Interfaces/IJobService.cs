using JobAgent.API.DTOs;
using JobAgent.API.Models;

namespace JobAgent.API.Services.Interfaces
{
    public interface IJobService
    {
        Task <List<Job>> MatchJobs(List<string> skills, int experience, List<string> education);
        Task<List<Job>> MatchJobsSemantic(string resumeFilePath);

        Task<Job> CreateJobAsync(JobDto request);
    }
}
