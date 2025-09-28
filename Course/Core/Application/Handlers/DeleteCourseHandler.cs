using Course.Core.Application.Commands;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;
        
        public DeleteCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await _courseRepository.DeleteAsync(request.Id);
        }
    }
}
