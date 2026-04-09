namespace JobAgent.API.Services.Interfaces
{
    public interface IAiService
    {
        Task<string> ParseResume(string filePath);

        Task<double> GetMatchScore(string resumeFilePath, string jobDescription);

        Task<List<double>> GetEmbedding(string text);
    }
}
