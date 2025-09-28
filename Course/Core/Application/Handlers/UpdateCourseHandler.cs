using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, CourseDTO?>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        
        public UpdateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        
        public async Task<CourseDTO?> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(request.Id);
            if (existingCourse == null) return null;
            
            _mapper.Map(request.CourseData, existingCourse);
            var updatedCourse = await _courseRepository.UpdateAsync(existingCourse);
            return _mapper.Map<CourseDTO>(updatedCourse);
        }
    }
}
