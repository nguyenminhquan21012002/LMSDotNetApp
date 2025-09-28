using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, CourseDTO?>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        
        public GetCourseByIdHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        
        public async Task<CourseDTO?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            return course == null ? null : _mapper.Map<CourseDTO>(course);
        }
    }
}
