namespace JobAgent.API.Models
{
    public class Job
    {
        public int Id { get; set; }  // primary key

        public string Title { get; set; }
        public string Company { get; set; }
        public List<string> RequiredSkills { get; set; } = new List<string>();
        public string Location { get; set; }
        public int MinExperience { get; set; }
        public string Education { get; set; }

        // NEW: AI Embedding
        public string Embedding { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
