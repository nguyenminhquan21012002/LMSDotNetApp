using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetLessonsPagedHandler : IRequestHandler<GetLessonsPagedQuery, BaseListResponse<LessonDTO>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public GetLessonsPagedHandler(ILessonRepository repository, IMapper mapper)
        {
            _lessonRepository = repository;
            _mapper = mapper;
        }

        public async Task<BaseListResponse<LessonDTO>> Handle(GetLessonsPagedQuery request, CancellationToken cancellationToken)
        {
            var (lessons, total) = await _lessonRepository.GetPagedAsync(
                request.Request.Page, 
                request.Request.Limit, 
                request.CourseId, 
                request.Request.SearchKey);

            var lessonDtos = _mapper.Map<IEnumerable<LessonDTO>>(lessons);
            return PaginationHelper.CreatePagedResponse(
                lessonDtos,
                total,
                request.Request,
                "Lessons retrieved successfully"
             );
        }
    }
}
