using MediatR;

namespace Course.Core.Application.Commands
{
    public class DeleteResourceCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteResourceCommand(string id)
        {
            Id = id;
        }
    }
}
