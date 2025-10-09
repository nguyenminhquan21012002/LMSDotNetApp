using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetResourcesByLessonIdQuery : IRequest<IEnumerable<ResourceDTO>>
    {
        public string LessonId { get; set; }

        public GetResourcesByLessonIdQuery(string lessonId)
        {
            LessonId = lessonId;
        }
    }
}
