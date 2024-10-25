namespace API.Helpers
{
    public class PaginationHeader(int currentPage, int totalPages, int pageSize, int totalCount)
    {
        public int CurrentPage { get; set; } = currentPage;
        public int TotalPages { get; set; } = totalPages;
        public int PageSize { get; set; } = pageSize;
        public int TotalCount { get; set; } = totalCount;
    }
}
