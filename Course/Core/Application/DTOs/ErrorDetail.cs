namespace Course.Core.Application.DTOs
{
    public class ErrorDetail
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}