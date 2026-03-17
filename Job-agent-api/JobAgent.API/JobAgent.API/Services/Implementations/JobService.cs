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

        public List<Job> MatchJobs(List<string> candidateSkills)
        {
            var allJobs = _jobRepository.GetAllJobs();
            return allJobs
                .Select(job =>
                {
                    int score = job.RequiredSkills.Count(skill => candidateSkills.Contains(skill, StringComparer.OrdinalIgnoreCase));
                    return new { job, score };
                })
                .Where(x => x.score > 0) // only jobs with at least one matching skill
                .OrderByDescending(x => x.score)
                .Select(x => x.job)
                .ToList();
        }
    }
}
