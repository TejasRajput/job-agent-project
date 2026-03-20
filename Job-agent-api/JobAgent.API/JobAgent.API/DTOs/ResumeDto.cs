namespace JobAgent.API.DTOs
{
    public class ResumeDto
    {
        public string FilePath { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
    }
}
