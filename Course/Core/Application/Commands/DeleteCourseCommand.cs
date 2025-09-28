using MediatR;

namespace Course.Core.Application.Commands
{
    public class DeleteCourseCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        
        public DeleteCourseCommand(Guid id)
        {
            Id = id;
        }
    }
}
