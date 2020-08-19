using System.Collections.Generic;
using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetProductsByCodeQuery: IRequest<IEnumerable<ProductResponse>>
    {
        public string ProductCode { get; set; }
        public GetProductsByCodeQuery( string productCode)
        {
            ProductCode = productCode;
        }
    }
}
