using Course.Core.Application.Commands;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class DeleteResourceHandler : IRequestHandler<DeleteResourceCommand, bool>
    {
        private readonly IResourceRepository _resourceRepository;

        public DeleteResourceHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<bool> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            return await _resourceRepository.DeleteAsync(request.Id);
        }
    }
}
