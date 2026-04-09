using JobAgent.API.DTOs;
using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;
using JobAgent.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace JobAgent.API.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository; 
        private readonly IAiService _aiService;
        private readonly IEmbeddingService _embeddingService;

        public JobService(IJobRepository jobRepository, IAiService aiService, IEmbeddingService embeddingService)
        {
            _jobRepository = jobRepository;
            _aiService = aiService;
            _embeddingService = embeddingService;
        }

        public async Task<Job> CreateJobAsync(JobDto request)
        {
            var jobText = $"{request.Title} {string.Join(", ", request.RequiredSkills)} {request.Education}";

            var embedding = await _aiService.GetEmbedding(jobText);
            var job = new Job
            {
                Title = request.Title,
                Company = request.Company,
                RequiredSkills = request.RequiredSkills,
                Location = request.Location,
                MinExperience = request.MinExperience,
                Education = request.Education,
                Embedding = JsonConvert.SerializeObject(embedding),
                CreatedAt = DateTime.UtcNow
            };
            await _jobRepository.AddJobAsync(job);

            return job;
        }

        public async Task<List<Job>> MatchJobs(List<string> skills, int experience, List<string> education)
        {
            var allJobs = await _jobRepository.GetAllJobsAsync();
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
            var allJobs = await _jobRepository.GetAllJobsAsync();

            // Step 1: Extract resume text once
            var resumeText = await _aiService.ParseResume(resumeFilePath);

            // Step 2: Get resume embedding once
            var resumeEmbedding = await _aiService.GetEmbedding(resumeText);

            var scoredJobs = new List<(Job job, double score)>();

            foreach (var job in allJobs)
            {
                // Combine full job info
                var jobText = $"{job.Title} {string.Join(", ", job.RequiredSkills)} {job.Education} {job.Location}";

                // Step 3: Get job embedding
                var jobEmbedding = await _aiService.GetEmbedding(jobText);

                // Step 4: Compute cosine similarity in .NET
                var score = _embeddingService.CosineSimilarity(resumeEmbedding, jobEmbedding);

                scoredJobs.Add((job, score));
            }

            // Step 5: Order by similarity
            return scoredJobs
                .OrderByDescending(x => x.score)
                .Where(x => x.score > 0.3) // optional threshold
                .Take(5)
                .Select(x => x.job)
                .ToList();
        }
    }
}
