using MediatR;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Application.DTOs.Request;

namespace Quiz.Core.Application.Commands
{
    public class CreateQuizCommand: IRequest<QuizDTO>
    {
        public CreateQuizDTO QuizData { get; set; }
        
        public CreateQuizCommand(CreateQuizDTO quizData)
        {
            QuizData = quizData;
        }
    }
}
