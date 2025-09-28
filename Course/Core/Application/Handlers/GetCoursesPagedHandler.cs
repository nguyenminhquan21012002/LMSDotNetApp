using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Application.Helpers;
using Course.Core.Application.Queries;
using Course.Core.Domain.Interfaces;
using MediatR;

namespace Course.Core.Application.Handlers
{
    public class GetCoursesPagedHandler : IRequestHandler<GetCoursesPagedQuery, BaseListResponse<CourseDTO>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        
        public GetCoursesPagedHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        
        public async Task<BaseListResponse<CourseDTO>> Handle(GetCoursesPagedQuery request, CancellationToken cancellationToken)
        {
            var (courses, total) = await _courseRepository.GetPagedAsync(
                request.Request.Page, 
                request.Request.Limit, 
                request.Request.SearchKey);
            
            var courseDtos = _mapper.Map<IEnumerable<CourseDTO>>(courses);
            
            return PaginationHelper.CreatePagedResponse(
                courseDtos, 
                total, 
                request.Request, 
                "Courses retrieved successfully");
        }
    }
}
