using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetAllLessonsQuery : IRequest<IEnumerable<LessonDTO>>
    {
    }
}
