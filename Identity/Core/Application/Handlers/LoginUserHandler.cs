using Identity.Core.Application.Commands;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Entities;
using Identity.Core.Domain.Interfaces;
using Identity.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResultDto?>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtTokenGenerator _jwt;

        public LoginUserHandler(IUserRepository repo, IPasswordHasher<User> hasher, JwtTokenGenerator jwt)
        {
            _repo = repo;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<AuthResultDto?> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await _repo.GetByEmailAsync(request.Email);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            var token = _jwt.GenerateToken(user.Id, user.Email, user.Role);

            return new AuthResultDto { Token = token };
        }
    }
}
