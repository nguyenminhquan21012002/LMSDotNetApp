using AutoMapper;
using MediatR;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Application.Queries;
using Quiz.Core.Domain.Interfaces;

namespace Quiz.Core.Application.Handlers
{
    public class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, QuizDTO?>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public GetQuizByIdQueryHandler(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<QuizDTO?> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.Id);
            return quiz == null ? null : _mapper.Map<QuizDTO>(quiz);
        }
    }
}

