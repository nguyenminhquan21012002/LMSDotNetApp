using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetResourcesByLessonIdHandler : IRequestHandler<GetResourcesByLessonIdQuery, IEnumerable<ResourceDTO>>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public GetResourcesByLessonIdHandler(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResourceDTO>> Handle(GetResourcesByLessonIdQuery request, CancellationToken cancellationToken)
        {
            // Simply get resources by lessonId, return empty list if none exist
            var resources = await _resourceRepository.GetByLessonIdAsync(request.LessonId);
            return _mapper.Map<IEnumerable<ResourceDTO>>(resources);
        }
    }
}
