using AspNetCoreWebApi.Core.Entities.Base;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Contract : AuditEntity
    {
        public int ContractId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int BillingAddressId { get; set; }
        public Address BillingAddress { get; set; }
        public int ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public ContractStatus Status { get; set; }
        public decimal GrandTotal { get; set; }

        public List<ContractItem> Items { get; set; } = new List<ContractItem>();

        // n-n relationships
        public IList<ContractPaymentAssociation> Payments { get; set; } = new List<ContractPaymentAssociation>();
    }

    public enum ContractStatus
    {
        OnGoing = 1,
        Closed = 2
    }
}
