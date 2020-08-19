using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, IPagedList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public SearchProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IPagedList<ProductResponse>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var productPagedList = await _productRepository.SearchProductsAsync(request.Args);
            return _mapper.Map<IPagedList<ProductResponse>>(productPagedList);
        }
    }
}
