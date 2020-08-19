using AspNetCoreWebApi.Core.Entities.Base;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Order : AuditEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int BillingAddressId { get; set; }
        public virtual Address BillingAddress { get; set; }
        public int ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public decimal GrandTotal { get; set; }

        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // n-n relationships
        public IList<OrderPaymentAssociation> Payments { get; set; } = new List<OrderPaymentAssociation>();
    }

    public enum OrderStatus
    {
        Draft = 1,
        Canceled = 2,
        Closed = 3
    }
}
