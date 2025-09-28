using Course.Core.Application.DTOs;

namespace Course.Core.Application.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, BaseRequest request)
        {
            request.Validate();
            return query.Skip(request.Skip).Take(request.Limit);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int page, int limit)
        {
            var skip = (page - 1) * limit;
            return query.Skip(skip).Take(limit);
        }

        public static BaseListResponse<T> CreatePagedResponse<T>(
            IEnumerable<T> data, 
            int total, 
            BaseRequest request, 
            string message = "Data retrieved successfully")
        {
            return ResponseHelper.SuccessListPaged(data, total, request.Page, request.Limit, message);
        }

        public static BaseListResponse<T> CreatePagedResponse<T>(
            IEnumerable<T> data, 
            int total, 
            int currentPage, 
            int pageSize, 
            string message = "Data retrieved successfully")
        {
            return ResponseHelper.SuccessListPaged(data, total, currentPage, pageSize, message);
        }

        public static int CalculateTotalPages(int total, int pageSize)
        {
            return (int)Math.Ceiling((double)total / pageSize);
        }

        public static bool HasNextPage(int currentPage, int totalPages)
        {
            return currentPage < totalPages;
        }

        public static bool HasPreviousPage(int currentPage)
        {
            return currentPage > 1;
        }

        public static PaginationInfo GetPaginationInfo(int total, int currentPage, int pageSize)
        {
            var totalPages = CalculateTotalPages(total, pageSize);
            return new PaginationInfo
            {
                Total = total,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasNextPage = HasNextPage(currentPage, totalPages),
                HasPreviousPage = HasPreviousPage(currentPage)
            };
        }
    }

    public class PaginationInfo
    {
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
