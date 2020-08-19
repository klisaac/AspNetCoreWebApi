using AspNetCoreWebApi.Core.Entities.Base;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Customer : AuditEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int DefaultAddressId { get; set; }
        public virtual Address DefaultAddress { get; set; }
        public string Email { get; set; }
        public string CitizenId { get; set; }
        public IList<Address> Addresses { get; set; } = new List<Address>();
    }
}
