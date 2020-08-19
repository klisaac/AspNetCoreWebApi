using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class OrderPaymentAssociation : AuditEntity
    {
        public int OrderPaymentAssociationId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
