using System;
using System.Collections.Generic;
using System.Linq;
using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;

namespace JobAgent.API.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository; 

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public List<Job> MatchJobs(List<string> skills, int experience, List<string> education)
        {
            var allJobs = _jobRepository.GetAllJobs();
            var rankedJobs = allJobs.Select(job =>
            {
                // Skill Score (50%)
                int skillMatch = job.RequiredSkills.Count(s =>
                    skills.Contains(s, StringComparer.OrdinalIgnoreCase));

                double skillScore = (double)skillMatch / job.RequiredSkills.Count * 50;

                // Experience Score (30%)
                double expScore = experience >= job.MinExperience ? 30 : 0;

                // Education Score (20%)
                double eduScore = education.Any(e =>
                    e.Contains(job.Education, StringComparison.OrdinalIgnoreCase)) ? 20 : 0;

                double totalScore = skillScore + expScore + eduScore;

                return new
                {
                    Job = job,
                    Score = totalScore
                };
            })
   .Where(x => x.Score > 0)
   .OrderByDescending(x => x.Score)
   .Take(5) // top 5 jobs
   .Select(x => x.Job)
   .ToList();

            return rankedJobs;
        }
    }
}
