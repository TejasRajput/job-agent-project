using JobAgent.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAgent.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        // Optional: if table names are different in DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .ToTable("Jobs"); // match your table name

            modelBuilder.Entity<Resume>()
                .ToTable("Resumes");
        }
    }
}
