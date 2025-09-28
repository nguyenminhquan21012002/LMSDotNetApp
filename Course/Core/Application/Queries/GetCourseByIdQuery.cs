using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetCourseByIdQuery : IRequest<CourseDTO?>
    {
        public Guid Id { get; set; }
        
        public GetCourseByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
