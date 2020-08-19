using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(int customerId)
            : base(o => o.CustomerId == customerId)
        {
            AddInclude(o => o.Customer);
            AddInclude("OrderItems.Product");
        }

        public OrderSpecification() : base(null)
        {
            AddInclude(o => o.Customer);
            AddInclude("OrderItems.Product");
        }
    }
}
