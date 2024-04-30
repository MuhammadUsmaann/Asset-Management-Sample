namespace AssetManagement.Server.Infrastructure.Helpers.Pagination
{
    public class PaginatedModel
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public string? SortColumn { get; set; }
        public DataSort Sort { get; set; } = DataSort.Descending;
    }
}
