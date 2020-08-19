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
    public class SearchCategoriesHandler : IRequestHandler<SearchCategoriesQuery, IPagedList<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SearchCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IPagedList<CategoryResponse>> Handle(SearchCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IPagedList<CategoryResponse>>(await _categoryRepository.SearchCategoriesAsync(request.Args));
        }
    }
}
