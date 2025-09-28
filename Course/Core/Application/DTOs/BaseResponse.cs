namespace Course.Core.Application.DTOs
{
    public class BaseResponse<T>
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ErrorDetail? Error { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T data, string message = "Success")
        {
            Status = "Success";
            Message = message;
            Data = data;
            Error = null;
        }

        public BaseResponse(string errorMessage, int errorCode, string status = "Error")
        {
            Status = status;
            Message = errorMessage;
            Data = default(T);
            Error = new ErrorDetail
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }
    }
}
