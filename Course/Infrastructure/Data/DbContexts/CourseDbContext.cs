using Microsoft.EntityFrameworkCore;
using Course.Core.Domain.Entities;

namespace Course.Infrastructure.Data.DbContexts
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
        }
        
        public DbSet<Courses> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
    }
}
