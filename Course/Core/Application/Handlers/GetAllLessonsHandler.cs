using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.Repositories;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetAllLessonsHandler : IRequestHandler<GetAllLessonsQuery, IEnumerable<LessonDTO>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public GetAllLessonsHandler(ILessonRepository repository, IMapper mapper)
        {
            _lessonRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDTO>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            var result = await _lessonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LessonDTO>>(result);
        }
    }
}
