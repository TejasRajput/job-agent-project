using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;
using JobAgent.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobAgent.API.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository; 
        private readonly IAiService _aiService;

        public JobService(IJobRepository jobRepository, IAiService aiService)
        {
            _jobRepository = jobRepository;
            _aiService = aiService;
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

        public async Task<List<Job>> MatchJobsSemantic(string resumeFilePath)
        {
            var allJobs = _jobRepository.GetAllJobs();
            var scoredJobs = new List<(Job job, double score)>();

            foreach (var job in allJobs)
            {
                // Combine title + skills for AI input
                var jobText = $"{job.Title} {string.Join(", ", job.RequiredSkills)}";

                // Call Python AI to get similarity
                var score = await _aiService.GetMatchScore(resumeFilePath, jobText);

                scoredJobs.Add((job, score));
            }

            // Order by highest score and return top matches
            return scoredJobs
                .OrderByDescending(x => x.score)
                .Take(5)
                .Select(x => x.job)
                .ToList();
        }
    }
}
