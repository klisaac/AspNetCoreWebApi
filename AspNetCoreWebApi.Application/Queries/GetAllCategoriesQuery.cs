using System.Collections.Generic;
using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetAllCategoriesQuery: IRequest<IEnumerable<CategoryResponse>>
    {
    }
}
