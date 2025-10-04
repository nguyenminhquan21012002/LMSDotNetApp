using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetCourseByIdQuery : IRequest<CourseDTO?>
    {
        public string Id { get; set; }
        
        public GetCourseByIdQuery(string id)
        {
            Id = id;
        }
    }
}
