using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Core.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IPagedList<Product>> SearchProductsAsync(SearchArgs args);
        Task<IEnumerable<Product>> GetProductByCodeAsync(string productCode);
        Task<Product> GetProductByIdWithCategoryAsync(int productId);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId);
    }
}
