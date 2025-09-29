using AutoMapper;
using Identity.Core.Domain.Entities;
using Identity.Core.Application.DTOs;

namespace Identity.Core.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
