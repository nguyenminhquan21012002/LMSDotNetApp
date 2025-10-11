using Course.Core.Domain.Enums;

namespace Course.Core.Domain.Entities
{
    public class Courses
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public CourseLevelEnum? Level { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CourseStatusEnum? Status { get; set; }
    }
}
