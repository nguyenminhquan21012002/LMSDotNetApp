using AutoMapper;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Domain.Entities;

namespace Quiz.Core.Application.Mappings
{
    public class QuizMappingProfile : Profile
    {
        public QuizMappingProfile()
        {
            // Quiz mappings
            CreateMap<Quiz.Core.Domain.Entities.Quizzes, QuizDTO>().ReverseMap();
        }
    }
}

