using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetProductByIdQuery: IRequest<ProductResponse>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
