using Identity.Core.Application.DTOs;
using MediatR;

namespace Identity.Core.Application.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
}
