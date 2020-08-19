using MediatR;

namespace AspNetCoreWebApi.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}
