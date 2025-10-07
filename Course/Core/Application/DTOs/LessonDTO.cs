namespace Course.Core.Application.DTOs
{
    public class LessonDTO
    {
        public string? Id { get; set; }
        public string? CourseId { get; set; }
        public string? Title { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
