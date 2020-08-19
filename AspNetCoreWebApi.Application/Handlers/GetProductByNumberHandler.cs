using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class GetProductsByNumberHandler : IRequestHandler<GetProductsByCodeQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByNumberHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByCodeQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetProductByCodeAsync(request.ProductCode));
        }
    }
}
