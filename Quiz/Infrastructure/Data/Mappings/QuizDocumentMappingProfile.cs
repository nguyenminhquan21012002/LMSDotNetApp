using AutoMapper;
using Quiz.Core.Domain.Entities;
using Quiz.Infrastructure.Data.MongoDbDocuments;

namespace Quiz.Infrastructure.Data.Mappings
{
    public class QuizDocumentMappingProfile : Profile
    {
        public QuizDocumentMappingProfile()
        {
            // Map from domain entity to MongoDB document and reverse
            CreateMap<Quizzes, QuizDocument>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Core.Domain.Enums.QuizType)src.Type));
        }
    }
}

