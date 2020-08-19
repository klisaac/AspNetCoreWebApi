using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Infrastructure.Repository.Base;
using AspNetCoreWebApi.Core.Specifications;

namespace AspNetCoreWebApi.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDataContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await GetAsync(new OrderSpecification(customerId));
        }

    }
}
