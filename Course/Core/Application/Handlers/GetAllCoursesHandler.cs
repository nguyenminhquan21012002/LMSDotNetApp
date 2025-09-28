using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDTO>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        
        public GetAllCoursesHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CourseDTO>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }
    }
}
