namespace Course.Core.Application.DTOs
{
    public class CreateLessonDTO
    {
        public string? CourseId { get; set; }
        public string? Title { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
    }
}
