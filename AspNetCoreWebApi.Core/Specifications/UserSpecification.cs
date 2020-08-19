using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string userName)
            : base(u => u.UserName == userName)
        {
            //AddInclude(u => u.Employee);
        }

        public UserSpecification(int userId)
            : base(u => u.UserId == userId)
        {
        }
        public UserSpecification() : base(null)
        {
        }
    }
}
