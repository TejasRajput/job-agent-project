using JobAgent.API.Services.Interfaces;

namespace JobAgent.API.Services.Implementations;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;

    public AiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ParseResume(string filePath)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "http://localhost:8000/parse-resume",
            new { path = filePath });

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<double> GetMatchScore(string resumeFilePath, string jobDescription)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "http://localhost:8000/match-job",
            new
            {
                resume_path = resumeFilePath,
                job_description = jobDescription
            });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, double>>();

        return result?["similarity_score"] ?? 0;
    }
}