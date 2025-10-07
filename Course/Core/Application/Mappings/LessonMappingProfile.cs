using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;

namespace Course.Core.Application.Mappings
{
    public class LessonMappingProfile: Profile
    {
        public LessonMappingProfile() {
            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<CreateLessonDTO, Lesson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<UpdateLessonDTO, Lesson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
