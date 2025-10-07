using Course.Core.Application.DTOs;
using LMSApp.Shared.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetLessonsPagedQuery : IRequest<BaseListResponse<LessonDTO>>
    {
        public BaseRequest Request { get; set; }
        public string? CourseId { get; set; }
        public GetLessonsPagedQuery(BaseRequest request, string? courseId)
        {
            Request = request;
            Request.Validate(); // Ensure valid pagination parameters
            CourseId = courseId;
        }
    }
}
