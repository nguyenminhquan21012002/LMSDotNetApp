using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Shared.Extensions
{
    public static class ControllerExtensions
    {
        // Success responses
        public static ActionResult<BaseResponse<T>> SuccessResponse<T>(this ControllerBase controller, T data, string message = "Operation completed successfully")
        {
            return controller.Ok(ResponseHelper.Success(data, message));
        }

        public static ActionResult<BaseListResponse<T>> SuccessListResponse<T>(this ControllerBase controller, IEnumerable<T> data, string message = "Data retrieved successfully")
        {
            return controller.Ok(ResponseHelper.SuccessList(data, message));
        }

        // Error responses
        public static ActionResult<BaseResponse<T>> BadRequestResponse<T>(this ControllerBase controller, string message = "Bad request")
        {
            return controller.BadRequest(ResponseHelper.BadRequest<T>(message));
        }

        public static ActionResult<BaseResponse<T>> NotFoundResponse<T>(this ControllerBase controller, string message = "Resource not found")
        {
            return controller.NotFound(ResponseHelper.NotFound<T>(message));
        }

        public static ActionResult<BaseResponse<T>> InternalServerErrorResponse<T>(this ControllerBase controller, string message = "Internal server error")
        {
            return controller.StatusCode(500, ResponseHelper.InternalServerError<T>(message));
        }

        public static ActionResult<BaseResponse<T>> ValidationErrorResponse<T>(this ControllerBase controller, string message)
        {
            return controller.UnprocessableEntity(ResponseHelper.ValidationError<T>(message));
        }

        // List error responses
        public static ActionResult<BaseListResponse<T>> BadRequestListResponse<T>(this ControllerBase controller, string message = "Bad request")
        {
            return controller.BadRequest(ResponseHelper.BadRequestList<T>(message));
        }

        public static ActionResult<BaseListResponse<T>> NotFoundListResponse<T>(this ControllerBase controller, string message = "Resources not found")
        {
            return controller.NotFound(ResponseHelper.NotFoundList<T>(message));
        }

        public static ActionResult<BaseListResponse<T>> InternalServerErrorListResponse<T>(this ControllerBase controller, string message = "Internal server error")
        {
            return controller.StatusCode(500, ResponseHelper.InternalServerErrorList<T>(message));
        }
    }
}
