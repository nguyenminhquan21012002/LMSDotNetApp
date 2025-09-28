using Microsoft.EntityFrameworkCore;
using Course.Core.Domain.Entities;

namespace Course.Infrastructure.Data.DbContexts
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
        }
        
        public DbSet<Core.Domain.Entities.Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Course configuration
            modelBuilder.Entity<Core.Domain.Entities.Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Instructor).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.Instructor);
                entity.HasIndex(e => e.Type);
            });
            
            // CourseModule configuration
            modelBuilder.Entity<CourseModule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(e => e.Course)
                      .WithMany(e => e.Modules)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
