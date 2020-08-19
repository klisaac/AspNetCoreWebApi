using System;
using AspNetCoreWebApi.Core.Pagination.Base;

namespace AspNetCoreWebApi.Core.Pagination
{
    public abstract class Pagination: BasePagination
    {
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
