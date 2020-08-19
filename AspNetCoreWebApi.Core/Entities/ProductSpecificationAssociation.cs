using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class ProductSpecificationAssociation : AuditEntity
    {
        public int ProductSpecificationAssociationId { get; set;}
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
    }
}
