using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(string name)
            : base(c => c.Name == name)
        {
        }

        public CategorySpecification(int categoryId)
            : base(c => c.CategoryId == categoryId)
        {
        }
        public CategorySpecification() : base(null)
        {
        }
    }
}
