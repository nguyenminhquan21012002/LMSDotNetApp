using MediatR;
using Quiz.Core.Application.Commands;
using Quiz.Core.Domain.Interfaces;

namespace Quiz.Core.Application.Handlers
{
    public class DeleteQuizHandler: IRequestHandler<DeleteQuizCommand, bool>
    {
        private readonly IQuizRepository _quizRepository;
        public DeleteQuizHandler(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task<bool> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            return await _quizRepository.DeleteAsync(request.Id);
        }
    }
}
