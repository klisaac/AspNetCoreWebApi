using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Specification : AuditEntity
    {
        public int SpecificationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
