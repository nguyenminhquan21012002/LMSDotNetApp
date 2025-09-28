using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetAllCoursesQuery : IRequest<IEnumerable<CourseDTO>>
    {
    }
}
