using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class UpdateResourceHandler : IRequestHandler<UpdateResourceCommand, ResourceDTO?>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public UpdateResourceHandler(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<ResourceDTO?> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var existingResource = await _resourceRepository.GetByIdAsync(request.Id);
            if (existingResource == null)
                return null;

            // Map only non-null properties from UpdateResourceDTO
            _mapper.Map(request.ResourceData, existingResource);
            
            var updatedResource = await _resourceRepository.UpdateAsync(existingResource);
            return _mapper.Map<ResourceDTO>(updatedResource);
        }
    }
}
