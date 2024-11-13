using Microsoft.EntityFrameworkCore;
using StudentProject.Models;

namespace StudentProject
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<Student> Students { get; set; }

    }
}
