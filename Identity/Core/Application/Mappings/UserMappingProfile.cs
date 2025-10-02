using AutoMapper;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Entities;

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
