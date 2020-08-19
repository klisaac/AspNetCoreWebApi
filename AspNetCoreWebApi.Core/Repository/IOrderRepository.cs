using System.Threading.Tasks;
using System.Collections.Generic;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Repository.Base;

namespace AspNetCoreWebApi.Core.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
    }
}
