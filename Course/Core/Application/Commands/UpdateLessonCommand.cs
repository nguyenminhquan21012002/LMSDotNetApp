using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class UpdateLessonCommand : IRequest<LessonDTO?>
    {
        public string Id { get; set; }
        public UpdateLessonDTO LessonData { get; set; }

        public UpdateLessonCommand(string id, UpdateLessonDTO lessonData)
        {
            Id = id;
            LessonData = lessonData;
        }
    }
}
