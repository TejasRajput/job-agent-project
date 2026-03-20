namespace JobAgent.API.Models
{
    public class Job
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public List<string> RequiredSkills { get; set; }   
        public string Location { get; set; }

        public int MinExperience { get; set; }
        public string Education { get; set; }
    }
}
