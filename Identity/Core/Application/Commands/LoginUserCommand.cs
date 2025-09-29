using Identity.Core.Application.DTOs;
using MediatR;

namespace Identity.Core.Application.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthResultDto?>;
}
