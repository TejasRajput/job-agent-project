using JobAgent.API.Services.Interfaces;

namespace JobAgent.API.Services.Implementations
{
    public class PythonEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;

        public PythonEmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<double>> GetEmbeddingAsync(string text)
        {
            var response = await _httpClient.PostAsJsonAsync("/generate-embedding", new
            {
                text = text
            });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();

            return result.Embedding;
        }

        public double CosineSimilarity(List<double> a, List<double> b)
        {
            double dot = 0, normA = 0, normB = 0;

            for (int i = 0; i < a.Count; i++)
            {
                dot += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
            }

            return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }

        public class EmbeddingResponse
        {
            public List<double> Embedding { get; set; }
        }
    }
}
