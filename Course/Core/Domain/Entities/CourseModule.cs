namespace Course.Core.Domain.Entities
{
    public class CourseModule
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Order { get; set; }
        public int Duration { get; set; } // in minutes
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Foreign key
        public Guid CourseId { get; set; }
        public Courses Course { get; set; } = null!;
    }
}
