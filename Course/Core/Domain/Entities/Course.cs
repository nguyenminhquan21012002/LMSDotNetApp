using Course.Core.Domain.Enums;

namespace Course.Core.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public CourseType Type { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; } // in minutes
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public List<CourseModule> Modules { get; set; } = new();
    }
}
