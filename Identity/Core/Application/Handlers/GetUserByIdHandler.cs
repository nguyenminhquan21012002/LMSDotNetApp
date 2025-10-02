using AutoMapper;
using Identity.Core.Application.DTOs;
using Identity.Core.Application.Queries;
using Identity.Core.Domain.Interfaces;
using MediatR;

namespace Identity.Core.Application.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }
    }
}