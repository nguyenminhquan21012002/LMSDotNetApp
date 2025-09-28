using Course.Core.Domain.Entities;

namespace Course.Core.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Entities.Course>> GetAllAsync();
        Task<Entities.Course?> GetByIdAsync(Guid id);
        Task<Entities.Course> CreateAsync(Entities.Course course);
        Task<Entities.Course> UpdateAsync(Entities.Course course);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Entities.Course>> GetByInstructorAsync(string instructor);
        Task<IEnumerable<Entities.Course>> GetByTypeAsync(Enums.CourseType type);
        
        // Pagination methods
        Task<(IEnumerable<Entities.Course> courses, int total)> GetPagedAsync(int page, int limit, string searchKey = "");
        Task<int> GetTotalCountAsync(string searchKey = "");
    }
}
