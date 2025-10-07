using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetLessonByIdQuery : IRequest<LessonDTO?>
    {
        public string Id { get; set; }

        public GetLessonByIdQuery(string id)
        {
            Id = id;
        }
    }
}