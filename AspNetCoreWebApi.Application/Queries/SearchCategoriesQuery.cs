using MediatR;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class SearchCategoriesQuery: IRequest<IPagedList<CategoryResponse>>
    {
        public SearchArgs Args { get; set; }
        public SearchCategoriesQuery(SearchArgs args)
        {
            Args = args;
        }
    }
}
