using MediatR;

namespace Quiz.Core.Application.Commands
{
    public class DeleteQuizCommand: IRequest<bool>
    {
        public string Id { get; set; }
        public DeleteQuizCommand(string id)
        {
            Id = id;
        }
    }
}
