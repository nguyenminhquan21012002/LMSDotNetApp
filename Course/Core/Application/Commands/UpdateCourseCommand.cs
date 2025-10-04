using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class UpdateCourseCommand : IRequest<CourseDTO?>
    {
        public string Id { get; set; }
        public UpdateCourseDTO CourseData { get; set; }
        
        public UpdateCourseCommand(string id, UpdateCourseDTO courseData)
        {
            Id = id;
            CourseData = courseData;
        }
    }
}
