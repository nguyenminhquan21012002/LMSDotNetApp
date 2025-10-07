using Course.Core.Application.Commands;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand, bool>
    {
        private readonly ILessonRepository _lessonRepository;

        public DeleteLessonHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<bool> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            return await _lessonRepository.DeleteAsync(request.Id);
        }
    }
}
