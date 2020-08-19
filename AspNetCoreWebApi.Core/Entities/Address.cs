using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Address : AuditEntity
    {
        public int AddressId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
