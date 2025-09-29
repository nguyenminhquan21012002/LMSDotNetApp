using Identity.Core.Application.Commands;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Interfaces;
using MediatR;

namespace Identity.Core.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResultDto?>
    {
        private readonly IUserRepository _repo;

        public LoginUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<AuthResultDto?> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await _repo.GetByEmailAsync(request.Email);
            if (user == null || user.PasswordHash != request.Password) return null; // TODO: verify hash

            return new AuthResultDto { Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) }; // TODO: real JWT
        }
    }
}
