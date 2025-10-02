using AutoMapper;
using Identity.Core.Application.Commands;
using Identity.Core.Application.DTOs;
using Identity.Core.Domain.Entities;
using Identity.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;

        public RegisterUserHandler(IUserRepository repo, IMapper mapper, IPasswordHasher<User> hasher)
        {
            _repo = repo;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByEmailAsync(request.Email);
            if (existing != null) throw new Exception("User already exists");

            var user = new User { Email = request.Email };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            await _repo.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
