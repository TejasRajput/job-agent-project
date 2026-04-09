using JobAgent.API.Data;
using JobAgent.API.Models;
using JobAgent.API.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace JobAgent.API.Services.Implementations
{
    public class JobMatchingService : IJobMatchingService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly ApplicationDbContext _db;

        public JobMatchingService(IEmbeddingService embeddingService, ApplicationDbContext db)
        {
            _embeddingService = embeddingService;
            _db = db;
        }

        public async Task<List<JobMatchResult>> GetMatchedJobsAsync(string resumeText)
        {
            // STEP 1: Get resume embedding (ONLY ONCE)
            var resumeEmbedding = await _embeddingService.GetEmbeddingAsync(resumeText);

            // STEP 2: Fetch jobs
            var jobs = await _db.Jobs.ToListAsync();

            // STEP 3: Calculate similarity
            var results = new List<JobMatchResult>();

            foreach (var job in jobs)
            {
                var jobEmbedding = JsonConvert.DeserializeObject<List<double>>(job.Embedding);

                var score = _embeddingService.CosineSimilarity(resumeEmbedding, jobEmbedding);

                results.Add(new JobMatchResult
                {
                    JobId = job.Id,
                    Title = job.Title,
                    Score = score
                });
            }

            // STEP 4: Rank
            return results.OrderByDescending(x => x.Score).ToList();
        }
    }
}
