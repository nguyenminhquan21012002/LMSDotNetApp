using MediatR;

namespace Course.Core.Application.Commands
{
    public class DeleteLessonCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteLessonCommand(string id)
        {
            Id = id;
        }
    }
}
