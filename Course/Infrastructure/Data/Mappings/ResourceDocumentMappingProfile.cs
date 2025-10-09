using AutoMapper;
using Course.Core.Domain.Entities;
using Course.Infrastructure.Data.MongoDbDocuments;

namespace Course.Infrastructure.Data.Mappings
{
    public class ResourceDocumentMappingProfile : Profile
    {
        public ResourceDocumentMappingProfile()
        {
            CreateMap<Resource, ResourceDocument>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type ?? 0));
        }
    }
}
