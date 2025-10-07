using Course.Core.Domain.Entities;

namespace Course.Core.Domain.Interfaces
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task<Lesson?> GetByIdAsync(string id);

        Task<IEnumerable<Lesson>> GetLessonsByCourseId(string courseId);
        Task<Lesson> CreateAsync(Lesson entity);
        Task<Lesson> UpdateAsync(Lesson entity);
        Task<bool> DeleteAsync(string id);
        Task<(IEnumerable<Lesson> lessons, long total)> GetPagedAsync(int page, int limit, string? CourseId, string searchKey = "");
        Task<Lesson> CheckExistLessonInTheOrder(string CourseId, int Order);
    }
}
