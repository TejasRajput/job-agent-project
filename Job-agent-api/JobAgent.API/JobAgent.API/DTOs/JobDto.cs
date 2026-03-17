namespace JobAgent.API.DTOs
{
    public class JobDto
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public List<string> RequiredSkills { get; set; }
        public string Location { get; set; }
    }
}
