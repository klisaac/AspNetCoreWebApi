using MediatR;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class SearchProductsQuery: IRequest<IPagedList<ProductResponse>>
    {
        public SearchArgs Args { get; set; }
        public SearchProductsQuery(SearchArgs args)
        {
            Args = args;
        }
    }
}
