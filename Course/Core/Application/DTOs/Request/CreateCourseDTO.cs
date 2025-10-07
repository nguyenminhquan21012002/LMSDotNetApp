namespace Course.Core.Application.DTOs
{
    public class CreateCourseDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Level { get; set; }
        public int Status { get; set; } = 1;
    }
}
