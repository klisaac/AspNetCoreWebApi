namespace AspNetCoreWebApi.Core.Pagination.Base
{
    public abstract class BasePagination : IBasePagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
    }
}
