using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Core.Repository.Base;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IPagedList<Category>> SearchCategoriesAsync(SearchArgs args);
    }
}
