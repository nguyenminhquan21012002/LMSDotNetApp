using Quiz.Core.Domain.Enums;

namespace Quiz.Core.Application.DTOs.Request
{
    public class CreateQuizDTO
    {
        public string? CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public QuizType? Type { get; set; }
        public int? TimeLimit { get; set; } // in seconds
        public double? TotalPoints { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
