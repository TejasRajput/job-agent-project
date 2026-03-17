using JobAgent.API.Models;
using JobAgent.API.Repositories.Interfaces;

namespace JobAgent.API.Repositories.Implementations
{
    public class JobRepository: IJobRepository
    {
        private static readonly List<Job> Jobs = new()
    {
        new Job { Title = "Software Engineer", Company = "ABC Corp", RequiredSkills = new List<string>{ "C#", ".NET", "SQL" }, Location = "Bangalore" },
        new Job { Title = "Frontend Developer", Company = "XYZ Pvt Ltd", RequiredSkills = new List<string>{ "Angular", "HTML", "CSS" }, Location = "Mumbai" },
        new Job { Title = "Cloud Engineer", Company = "AzureTech", RequiredSkills = new List<string>{ "Azure", ".NET", "C#" }, Location = "Hyderabad" }
    };

        public List<Job> GetAllJobs() => Jobs;
    }
}
