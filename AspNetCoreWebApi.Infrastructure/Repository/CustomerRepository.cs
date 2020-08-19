using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Infrastructure.Repository.Base;

namespace AspNetCoreWebApi.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
