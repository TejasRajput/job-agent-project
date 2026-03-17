namespace JobAgent.API.Models
{
    public class Resume
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }
    }
}
