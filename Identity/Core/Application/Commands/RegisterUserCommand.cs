using Identity.Core.Application.DTOs;
using MediatR;

namespace Identity.Core.Application.Commands
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<UserDto>;
}
