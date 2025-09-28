using Course.Core.Domain.Entities;
using Course.Core.Domain.Enums;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Course.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;
        
        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Core.Domain.Entities.Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .Where(c => c.IsActive)
                .ToListAsync();
        }
        
        public async Task<Core.Domain.Entities.Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }
        
        public async Task<Core.Domain.Entities.Course> CreateAsync(Core.Domain.Entities.Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        
        public async Task<Core.Domain.Entities.Course> UpdateAsync(Core.Domain.Entities.Course course)
        {
            course.UpdatedAt = DateTime.UtcNow;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;
            
            course.IsActive = false;
            course.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<IEnumerable<Core.Domain.Entities.Course>> GetByInstructorAsync(string instructor)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .Where(c => c.Instructor == instructor && c.IsActive)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Core.Domain.Entities.Course>> GetByTypeAsync(CourseType type)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .Where(c => c.Type == type && c.IsActive)
                .ToListAsync();
        }
    }
}
