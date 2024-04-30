using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Server.Infrastructure.Helpers.Pagination
{
    /// <summary>
    /// To paginate an object collection List<T> or IQueryable<T>
    /// </summary>
    public class PaginationList<T>
    {
        public int PageIndex { get; set; }
        public string SortColumn { get; set; }
        public DataSort Sort { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }

        public PaginationList(List<T> items, int count, PaginatedModel paginatedModel)
        {
            PageIndex = paginatedModel.PageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)paginatedModel.PageSize);
            Items.AddRange(items);
            Sort = paginatedModel.Sort;
            SortColumn = paginatedModel.SortColumn;
        }
        public PaginationList()
        {
        }

        public bool HasPreviousPage
        {
            get => PageIndex > 1;
        }

        public bool HasNextPage
        {
            get => PageIndex < TotalPages;
        }

        public PaginationList<T> SetEmpty() => new PaginationList<T>(new List<T>(), 0, new PaginatedModel());

        //public PaginationList<T> Create(
        //   IQueryable<T> source, PaginatedModel paginatedModel)
        //{
        //    var count = source.Count();

        //    if (paginatedModel.Sort == DataSort.Ascending)
        //        source = source.OrderByAsc(paginatedModel.SortColumn);
        //    else
        //        source = source.OrderByDesc(paginatedModel.SortColumn);

        //    var query = source.Skip(
        //        (paginatedModel.PageIndex - 1) * paginatedModel.PageSize)
        //        .Take(paginatedModel.PageSize);
        //    var items = query.ToList();
        //    return new PaginationList<T>(items, count, paginatedModel);
        //}
    }
}
