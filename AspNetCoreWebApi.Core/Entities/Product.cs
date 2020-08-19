using AspNetCoreWebApi.Core.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Product : AuditEntity
    {
        public int ProductId { get; set; }
        //[Required, StringLength(80)]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Star { get; set; }

        // n-1 relationships
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // n-n relationships
        public IList<ProductSpecificationAssociation> Specifications { get; set; } = new List<ProductSpecificationAssociation>();

        public static Product Create(int productId, int categoryId, string name, decimal unitPrice = 0, short? unitsInStock = null)
        {
            var product = new Product
            {
                ProductId = productId,
                CategoryId = categoryId,
                Name = name,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock
            };
            return product;
        }
    }
}
