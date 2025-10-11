using Course.Core.Domain.Entities;
using Course.Core.Domain.Enums;

namespace Course.Core.Application.DTOs
{
    public class ResourceDTO
    {
        public string? Id { get; set; }
        public string? LessonId { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public ResourceTypeEnum? Type { get; set; }
        public Metadata? Metadata { get; set; }
    }
}
