using AutoMapper;
using Identity.Core.Application.Commands;
using Identity.Core.Domain.Entities;
using Identity.Core.Domain.Interfaces;
using Identity.Core.Application.DTOs;
using MediatR;

namespace Identity.Core.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByEmailAsync(request.Email);
            if (existing != null) throw new Exception("User already exists");

            var user = new User { Email = request.Email, PasswordHash = request.Password };
            await _repo.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
