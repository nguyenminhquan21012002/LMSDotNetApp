using AutoMapper;
using MediatR;
using Quiz.Core.Application.Commands;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Domain.Entities;
using Quiz.Core.Domain.Interfaces;

namespace Quiz.Core.Application.Handlers
{
    public class UpdateQuizHandler : IRequestHandler<UpdateQuizCommand, QuizDTO?>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizHandler> _logger;

        public UpdateQuizHandler(IQuizRepository quizRepository, IMapper mapper, ILogger<UpdateQuizHandler> logger)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QuizDTO?> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if quiz exists
                var existingQuiz = await _quizRepository.GetByIdAsync(request.Id);
                if (existingQuiz == null)
                {
                    _logger.LogWarning($"Quiz with ID {request.Id} not found");
                    return null;
                }

                // Map UpdateQuizDTO vào existingQuiz (chỉ update các field không null)
                _mapper.Map(request.QuizData, existingQuiz);
                
                // Set Id từ route parameter
                existingQuiz.Id = request.Id;

                // Update quiz
                var updatedQuiz = await _quizRepository.UpdateAsync(existingQuiz);

                // Map back to DTO
                var result = _mapper.Map<QuizDTO>(updatedQuiz);
                
                _logger.LogInformation($"Quiz {request.Id} updated successfully");
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating quiz {request.Id}");
                throw;
            }
        }
    }
}

