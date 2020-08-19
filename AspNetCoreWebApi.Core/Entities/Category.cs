using AspNetCoreWebApi.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Core.Entities
{
    public class Category : AuditEntity
    {
        public int CategoryId { get; set; }
        //[Required, StringLength(80)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public static Category Create(int categoryId, string name, string description = null)
        {
            var category = new Category
            {
                CategoryId = categoryId,
                Name = name,
                Description = description
            };
            return category;
        }
    }
}
