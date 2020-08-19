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
using AspNetCoreWebApi.Core.Specifications;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUser currentUser, IMapper mapper, IAppLogger<CreateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetSingleAsync(new CategorySpecification(request.Name)) != null)
                throw new BadRequestException("Category name already exists.");
                
            var category = _mapper.Map<Category>(request);
            category.IsDeleted = false;
            category.CreatedBy = _currentUser.UserName;

            var categoryResponse = _mapper.Map<CategoryResponse>(await _categoryRepository.AddAsync(category));
            _logger.LogInformation($"Created category, {JsonSerializer.Serialize(categoryResponse)}.");

            return categoryResponse;
        }
    }
}