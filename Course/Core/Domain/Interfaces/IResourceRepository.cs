using Course.Core.Domain.Entities;

namespace Course.Core.Domain.Interfaces
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetByLessonIdAsync(string lessonId);
        Task<Resource?> GetByIdAsync(string id);
        Task<Resource> CreateAsync(Resource resource);
        Task<Resource> UpdateAsync(Resource resource);
        Task<bool> DeleteAsync(string id);
    }
}
