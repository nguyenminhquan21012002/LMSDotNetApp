using AutoMapper;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Application.DTOs.Request;
using Quiz.Core.Domain.Entities;

namespace Quiz.Core.Application.Mappings
{
    public class QuizMappingProfile : Profile
    {
        public QuizMappingProfile()
        {
            // Quiz mappings
            CreateMap<Quizzes, QuizDTO>().ReverseMap();
            
            // CreateQuizDTO to Quizzes
            CreateMap<CreateQuizDTO, Quizzes>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            // UpdateQuizDTO to Quizzes
            CreateMap<UpdateQuizDTO, Quizzes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

