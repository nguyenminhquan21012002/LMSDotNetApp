namespace Course.Core.Application.DTOs
{
    public class BaseListResponse<T>
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public IList<T>? Data { get; set; }
        public ErrorDetail? Error { get; set; }

        public BaseListResponse()
        {
        }

        public BaseListResponse(List<T> data, string message = "Success")
        {
            Status = "Success";
            Message = message;
            Data = data;
            Total = data.Count;
            TotalPages = 1;
            CurrentPage = 1;
            Error = null;
        }

        public BaseListResponse(IEnumerable<T> data, string message = "Success")
        {
            Status = "Success";
            Message = message;
            Data = data.ToList();
            Total = data.Count();
            TotalPages = 1;
            CurrentPage = 1;
            Error = null;
        }

        public BaseListResponse(IEnumerable<T> data, int total, int currentPage, int pageSize, string message = "Success")
        {
            Status = "Success";
            Message = message;
            Data = data.ToList();
            Total = total;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling((double)total / pageSize);
            Error = null;
        }

        public BaseListResponse(string errorMessage, int errorCode, string status = "Error")
        {
            Status = status;
            Message = errorMessage;
            Data = null;
            Total = 0;
            TotalPages = 0;
            CurrentPage = 0;
            Error = new ErrorDetail
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }
    }
}
