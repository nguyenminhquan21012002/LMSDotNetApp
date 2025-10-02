using Course.Core.Application.DTOs;
using LMSApp.Shared.DTOs;
using MediatR;

namespace Course.Core.Application.Queries
{
    public class GetCoursesPagedQuery : IRequest<BaseListResponse<CourseDTO>>
    {
        public BaseRequest Request { get; set; }
        
        public GetCoursesPagedQuery(BaseRequest request)
        {
            Request = request;
            Request.Validate(); // Ensure valid pagination parameters
        }
    }
}
