using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetLessonsByCourseIdHandler : IRequestHandler<GetLessonsByCourseIdQuery, IEnumerable<LessonDTO>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetLessonsByCourseIdHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDTO>> Handle(GetLessonsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            // Check if course exists
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentException($"Course with ID {request.CourseId} not found");
            }

            var lessons = await _lessonRepository.GetLessonsByCourseId(request.CourseId);
            return _mapper.Map<IEnumerable<LessonDTO>>(lessons);
        }
    }
}
