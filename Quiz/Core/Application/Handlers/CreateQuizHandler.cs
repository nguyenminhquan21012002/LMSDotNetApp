using AutoMapper;
using MediatR;
using Quiz.Core.Application.Commands;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Domain.Entities;
using Quiz.Core.Domain.Interfaces;

namespace Quiz.Core.Application.Handlers
{
    public class CreateQuizHandler : IRequestHandler<CreateQuizCommand, QuizDTO>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuizHandler> _logger;

        public CreateQuizHandler(IQuizRepository quizRepository, IMapper mapper, ILogger<CreateQuizHandler> logger)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QuizDTO> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map DTO to Entity
                var quiz = _mapper.Map<Quizzes>(request.QuizData);
                
                // Set CreatedAt if not provided
                if (quiz.CreatedAt == default)
                {
                    quiz.CreatedAt = DateTime.UtcNow;
                }

                // Create quiz
                var createdQuiz = await _quizRepository.CreateAsync(quiz);

                // Map back to DTO
                var result = _mapper.Map<QuizDTO>(createdQuiz);
                
                _logger.LogInformation($"Quiz {result.Id} created successfully");
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quiz");
                throw;
            }
        }
    }
}
