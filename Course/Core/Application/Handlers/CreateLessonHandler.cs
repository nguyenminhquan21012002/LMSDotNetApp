using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class CreateLessonHandler : IRequestHandler<CreateLessonCommand, LessonDTO>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CreateLessonHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<LessonDTO> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = _mapper.Map<Lesson>(request.LessonData);
            var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
            if (course == null)
            {
                throw new ArgumentException($"There is no Course with the Id provided: {lesson.CourseId}");
            }
            if (lesson.Order != null)
            {
                var existingLesson = await _lessonRepository.CheckExistLessonInTheOrder(lesson.CourseId, lesson.Order.Value);
                if (existingLesson != null)
                {
                    throw new ArgumentException($"A lesson already exists at order {lesson.Order} for course {lesson.CourseId}");
                }
            }

            lesson.CreatedAt = DateTime.UtcNow;
            lesson.UpdatedAt = DateTime.UtcNow;

            var createdLesson = await _lessonRepository.CreateAsync(lesson);
            return _mapper.Map<LessonDTO>(createdLesson);
        }
    }
}
