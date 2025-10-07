using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class CreateLessonCommand : IRequest<LessonDTO>
    {
        public CreateLessonDTO LessonData { get; set; }

        public CreateLessonCommand(CreateLessonDTO createLessonData)
        {
            LessonData = createLessonData;
        }
    }
}
