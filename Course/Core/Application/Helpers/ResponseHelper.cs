using Course.Core.Application.DTOs;

namespace Course.Core.Application.Helpers
{
    public static class ResponseHelper
    {
        // Success responses
        public static BaseResponse<T> Success<T>(T data, string message = "Operation completed successfully")
        {
            return new BaseResponse<T>(data, message);
        }

        public static BaseListResponse<T> SuccessList<T>(IEnumerable<T> data, string message = "Data retrieved successfully")
        {
            var dataList = data.ToList();
            return new BaseListResponse<T>(dataList, dataList.Count, 1, dataList.Count, message);
        }

        public static BaseListResponse<T> SuccessListPaged<T>(IEnumerable<T> data, int total, int currentPage, int pageSize, string message = "Data retrieved successfully")
        {
            return new BaseListResponse<T>(data, total, currentPage, pageSize, message);
        }

        // Error responses with standard HTTP status codes
        public static BaseResponse<T> BadRequest<T>(string message = "Bad request", int errorCode = 400)
        {
            return new BaseResponse<T>(message, errorCode, "BadRequest");
        }

        public static BaseResponse<T> NotFound<T>(string message = "Resource not found", int errorCode = 404)
        {
            return new BaseResponse<T>(message, errorCode, "NotFound");
        }

        public static BaseResponse<T> InternalServerError<T>(string message = "Internal server error", int errorCode = 500)
        {
            return new BaseResponse<T>(message, errorCode, "InternalServerError");
        }

        public static BaseResponse<T> Unauthorized<T>(string message = "Unauthorized access", int errorCode = 401)
        {
            return new BaseResponse<T>(message, errorCode, "Unauthorized");
        }

        public static BaseResponse<T> Forbidden<T>(string message = "Access forbidden", int errorCode = 403)
        {
            return new BaseResponse<T>(message, errorCode, "Forbidden");
        }

        // List error responses
        public static BaseListResponse<T> BadRequestList<T>(string message = "Bad request", int errorCode = 400)
        {
            return new BaseListResponse<T>(message, errorCode, "BadRequest");
        }

        public static BaseListResponse<T> NotFoundList<T>(string message = "Resources not found", int errorCode = 404)
        {
            return new BaseListResponse<T>(message, errorCode, "NotFound");
        }

        public static BaseListResponse<T> InternalServerErrorList<T>(string message = "Internal server error", int errorCode = 500)
        {
            return new BaseListResponse<T>(message, errorCode, "InternalServerError");
        }

        // Validation error response
        public static BaseResponse<T> ValidationError<T>(string message, int errorCode = 422)
        {
            return new BaseResponse<T>(message, errorCode, "ValidationError");
        }

        public static BaseListResponse<T> ValidationErrorList<T>(string message, int errorCode = 422)
        {
            return new BaseListResponse<T>(message, errorCode, "ValidationError");
        }
    }
}
