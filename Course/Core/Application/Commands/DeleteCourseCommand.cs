using MediatR;

namespace Course.Core.Application.Commands
{
    public class DeleteCourseCommand : IRequest<bool>
    {
        public string Id { get; set; }
        
        public DeleteCourseCommand(string id)
        {
            Id = id;
        }
    }
}
