using Quiz.Core.Domain.Entities;

namespace Quiz.Core.Domain.Interfaces
{
    public interface IQuizRepository
    {
        Task<Quizzes?> GetByIdAsync(string id);
    }
}
