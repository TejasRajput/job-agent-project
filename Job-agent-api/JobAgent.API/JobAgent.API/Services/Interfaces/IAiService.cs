namespace JobAgent.API.Services.Interfaces
{
    public interface IAiService
    {
        Task<string> ParseResume(string filePath);
    }
}
