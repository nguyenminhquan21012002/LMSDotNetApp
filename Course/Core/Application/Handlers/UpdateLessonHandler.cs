using AutoMapper;
using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class UpdateLessonHandler : IRequestHandler<UpdateLessonCommand, LessonDTO?>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public UpdateLessonHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<LessonDTO?> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var existingLesson = await _lessonRepository.GetByIdAsync(request.Id);
            if (existingLesson == null)
                return null;

            // Check if order is being changed and if the new order conflicts
            if (request.LessonData.Order != null && request.LessonData.Order != existingLesson.Order)
            {
                var lessonInOrder = await _lessonRepository.CheckExistLessonInTheOrder(
                    existingLesson.CourseId, 
                    request.LessonData.Order.Value
                );
                
                if (lessonInOrder != null && lessonInOrder.Id != existingLesson.Id)
                {
                    throw new ArgumentException($"A lesson already exists at order {request.LessonData.Order} for this course");
                }
            }

            _mapper.Map(request.LessonData, existingLesson);
            existingLesson.UpdatedAt = DateTime.UtcNow;
            
            var updatedLesson = await _lessonRepository.UpdateAsync(existingLesson);
            return _mapper.Map<LessonDTO>(updatedLesson);
        }
    }
}
