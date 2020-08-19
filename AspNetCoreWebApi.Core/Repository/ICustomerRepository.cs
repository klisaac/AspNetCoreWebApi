using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Core.Repository.Base;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Core.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
    }
}
