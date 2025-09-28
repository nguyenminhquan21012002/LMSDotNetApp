using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, CourseDTO>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        
        public CreateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        
        public async Task<CourseDTO> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Domain.Entities.Course>(request.CourseData);
            course.Id = Guid.NewGuid();
            course.CreatedAt = DateTime.UtcNow;
            course.UpdatedAt = DateTime.UtcNow;
            
            var createdCourse = await _courseRepository.CreateAsync(course);
            return _mapper.Map<CourseDTO>(createdCourse);
        }
    }
}
