using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetLessonsByCourseIdQuery : IRequest<IEnumerable<LessonDTO>>
    {
        public string CourseId { get; set; }

        public GetLessonsByCourseIdQuery(string courseId)
        {
            CourseId = courseId;
        }
    }
}
