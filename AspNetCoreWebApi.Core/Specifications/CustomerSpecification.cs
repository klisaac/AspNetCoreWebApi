using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Specifications
{
    public class CustomerSpecification : BaseSpecification<Customer>
    {
        public CustomerSpecification(string name)
            : base(c => c.Name == name)
        {
            AddInclude(c => c.DefaultAddress);
        }

        public CustomerSpecification(int customerId)
            : base(c => c.CustomerId == customerId)
        {
            AddInclude(c => c.DefaultAddress);
        }
        public CustomerSpecification() : base(null)
        {
        }
    }
}
