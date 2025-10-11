using Quiz.Core.Domain.Entities;

namespace Quiz.Core.Domain.Interfaces
{
    public interface IQuizRepository
    {
        Task<Quizzes?> GetByIdAsync(string id);
        Task<Quizzes> CreateAsync(Quizzes quiz);
        Task<Quizzes?> UpdateAsync(Quizzes quiz);
        Task<bool> DeleteAsync(string id);
    }
}
