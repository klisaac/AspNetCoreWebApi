using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Repository.Base;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByName(ISpecification<User> specification);
        Task<bool> RegisterUser(User user);
    }
}
