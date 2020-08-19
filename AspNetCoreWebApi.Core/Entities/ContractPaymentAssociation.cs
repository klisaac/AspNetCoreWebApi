using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class ContractPaymentAssociation : AuditEntity
    {
        public int ContractPaymentAssociationId { get; set; }
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
