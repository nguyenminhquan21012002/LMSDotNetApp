using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;

namespace Course.Core.Application.Mappings
{
    public class ResourceMappingProfile : Profile
    {
        public ResourceMappingProfile()
        {
            CreateMap<Resource, ResourceDTO>().ReverseMap();
            
            CreateMap<CreateResourceDTO, Resource>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<UpdateResourceDTO, Resource>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LessonId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
