namespace JobAgent.API.Services.Interfaces
{
    public interface IEmbeddingService
    {
        Task<List<double>> GetEmbeddingAsync(string text);
        double CosineSimilarity(List<double> v1, List<double> v2);
    }
}
