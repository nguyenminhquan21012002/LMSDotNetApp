using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetLessonByIdHandler : IRequestHandler<GetLessonByIdQuery, LessonDTO?>
    {
        private readonly ILessonRepository _repository;
        private readonly IMapper _mapper;

        public GetLessonByIdHandler(ILessonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LessonDTO?> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _repository.GetByIdAsync(request.Id);
            return lesson != null ? _mapper.Map<LessonDTO>(lesson) : null;
        }
    }
}
