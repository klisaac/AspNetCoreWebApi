using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Common.Exceptions;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IAppLogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IAppLogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new BadRequestException($"Product with productId, {request.ProductId} does not exist.");

            product.LastModifiedBy = _currentUser.UserName;
            product.IsDeleted = true;
            var result = await _productRepository.UpdateAsync(product);
            _logger.LogInformation($"Deleted product with productId, {request.ProductId}.");
            return result != null;
        }
    }
}
