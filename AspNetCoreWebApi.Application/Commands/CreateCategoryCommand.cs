using MediatR;
using AspNetCoreWebApi.Application.Models;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
