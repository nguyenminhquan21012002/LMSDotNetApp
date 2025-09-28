using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;

namespace Course.Core.Application.Mappings
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Domain.Entities.Course, CourseDTO>();
            CreateMap<CourseModule, CourseModuleDTO>();
            CreateMap<CreateCourseDTO, Domain.Entities.Course>();
            CreateMap<UpdateCourseDTO, Domain.Entities.Course>();
        }
    }
}
