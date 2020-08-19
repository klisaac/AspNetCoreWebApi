using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Common.Exceptions;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAppLogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProducts = await _productRepository.GetProductByCodeAsync(request.Code);
            if (existingProducts != null && existingProducts.Count() > 0)
                throw new BadRequestException("Product code already exists.");
                
            var product = _mapper.Map<Product>(request);
            product.IsDeleted = false;
            product.CreatedBy = _currentUser.UserName;

            var productResponse = _mapper.Map<ProductResponse>(await _productRepository.AddByLoadingReferenceAsync(product, p => p.Category));
            _logger.LogInformation($"Created product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}