using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Core.Specifications.Base;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Infrastructure.Repository.Base;

namespace AspNetCoreWebApi.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDataContext AspNetCoreWebApiContext)
            : base(AspNetCoreWebApiContext)
        {
        }

        public override async Task<User> GetByIdAsync(int userId)
        {
            //TODO: should be refactored
            var users = await GetAsync(u => u.UserId == userId && u.IsDeleted == false);
            return users.FirstOrDefault();
        }
        public async Task<User> GetUserByName(ISpecification<User> specification)
        {
            return await base.GetSingleAsync(specification);
        }

        public async Task<bool> RegisterUser(User user)
        {
            await base.AddAsync(user);
            return true;
        }

    }
}
