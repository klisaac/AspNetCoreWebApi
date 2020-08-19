using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetProductsByCategoryIdQuery: IRequest<ProductResponse>
    {
        public int CategoryId { get; set; }

        public GetProductsByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
