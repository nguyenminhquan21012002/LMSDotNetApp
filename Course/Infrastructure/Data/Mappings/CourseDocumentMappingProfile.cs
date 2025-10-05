using AutoMapper;
using Course.Core.Domain.Entities;
using Course.Infrastructure.Data.MongoDbDocuments;

namespace Course.Infrastructure.Data.Mappings
{
    public class CourseDocumentMappingProfile: Profile
    {
        public CourseDocumentMappingProfile()
        {
            // Map from domain entity to MongoDB document
            CreateMap<Courses, CourseDocument>().ReverseMap();

            // Map from Courses -> CourseDocument
            //CreateMap<Courses, CourseDocument>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            //    .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            //// Map from MongoDB document back to domain entity
            //CreateMap<CourseDocument, Courses>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            //    .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
