using Course.Core.Domain.Entities;

namespace Course.Core.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Courses>> GetAllAsync();
        Task<Courses?> GetByIdAsync(string id);
        Task<Courses> CreateAsync(Courses course);
        Task<Courses> UpdateAsync(Courses course);
        Task<bool> DeleteAsync(string id);

        // Pagination methods
        Task<(IEnumerable<Courses> courses, long total)> GetPagedAsync(int page, int limit, string searchKey = "");
        Task<int> GetTotalCountAsync(string searchKey = "");
    }
}
