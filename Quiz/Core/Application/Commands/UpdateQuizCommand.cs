using MediatR;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Application.DTOs.Request;

namespace Quiz.Core.Application.Commands
{
    public class UpdateQuizCommand : IRequest<QuizDTO?>
    {
        public string Id { get; set; }
        public UpdateQuizDTO QuizData { get; set; }
        
        public UpdateQuizCommand(string id, UpdateQuizDTO quizData)
        {
            Id = id;
            QuizData = quizData;
        }
    }
}

