using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
