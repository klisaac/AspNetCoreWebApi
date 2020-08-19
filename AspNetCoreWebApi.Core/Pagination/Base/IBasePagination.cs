
namespace AspNetCoreWebApi.Core.Pagination
{
    public interface IBasePagination
    {
        int PageIndex { get; }
        int PageSize { get; }
        string SortBy { get; }
    }
}
