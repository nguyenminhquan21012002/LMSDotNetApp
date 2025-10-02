namespace LMSApp.Shared.DTOs
{
    public class BaseRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string SearchKey { get; set; } = string.Empty;

        // Calculated properties
        public int Skip => (Page - 1) * Limit;
        
        // Validation
        public void Validate()
        {
            if (Page < 1) Page = 1;
            if (Limit < 1) Limit = 10;
            if (Limit > 100) Limit = 100; // Max limit to prevent abuse
        }
    }

    public class PagedRequest : BaseRequest
    {
        // Additional properties for specific paged requests can be added here
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }
}
