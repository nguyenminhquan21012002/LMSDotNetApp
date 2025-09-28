namespace Course.Core.Domain.Events
{
    public class CourseCreatedEvent
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        public CourseCreatedEvent(Guid courseId, string title, string instructor, DateTime createdAt)
        {
            CourseId = courseId;
            Title = title;
            Instructor = instructor;
            CreatedAt = createdAt;
        }
    }
}
