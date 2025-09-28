using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class CreateCourseCommand : IRequest<CourseDTO>
    {
        public CreateCourseDTO CourseData { get; set; }
        
        public CreateCourseCommand(CreateCourseDTO courseData)
        {
            CourseData = courseData;
        }
    }
}
