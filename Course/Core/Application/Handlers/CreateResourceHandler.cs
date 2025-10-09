using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, ResourceDTO>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public CreateResourceHandler(IResourceRepository resourceRepository, ILessonRepository lessonRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<ResourceDTO> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            // Validate lesson exists
            var lesson = await _lessonRepository.GetByIdAsync(request.ResourceData.LessonId);
            if (lesson == null)
            {
                throw new ArgumentException($"Lesson with ID {request.ResourceData.LessonId} not found");
            }

            var resource = _mapper.Map<Resource>(request.ResourceData);
            var createdResource = await _resourceRepository.CreateAsync(resource);
            return _mapper.Map<ResourceDTO>(createdResource);
        }
    }
}
