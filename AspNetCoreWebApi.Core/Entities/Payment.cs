using System.Collections.Generic;
using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Payment : AuditEntity
    {
        public int PaymentId { get; set; }
        public decimal GrandTotal { get; set; }
        public List<PaymentItem> Items { get; set; } = new List<PaymentItem>();
    }
}
