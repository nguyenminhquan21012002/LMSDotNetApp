using MediatR;
using Quiz.Core.Application.DTOs;

namespace Quiz.Core.Application.Queries
{
    public class GetQuizByIdQuery : IRequest<QuizDTO?>
    {
        public string Id { get; set; }

        public GetQuizByIdQuery(string id)
        {
            Id = id;
        }
    }
}

