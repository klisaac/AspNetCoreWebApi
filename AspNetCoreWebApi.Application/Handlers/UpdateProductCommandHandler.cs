using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Common.Exceptions;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAppLogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new BadRequestException($"Product with productId, {request.ProductId} does not exist.");

            product.Code = request.Code;
            product.Name = request.Name;
            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.CategoryId = request.CategoryId;
            product.LastModifiedBy = _currentUser.UserName;
            var productResponse = _mapper.Map<ProductResponse> (await _productRepository.UpdateByLoadingReferenceAsync(product, p => p.Category));
            _logger.LogInformation($"Updated product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}