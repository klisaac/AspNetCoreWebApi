using System;
using System.Linq.Expressions;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification() : base(null)
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(int productId) : base(p => p.ProductId == productId)
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(string productCode) : base(p => p.Code.Contains(productCode))
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(int? productId, string productName) : base(null)
        {
            AddInclude(p => p.Category);
            Expression<Func<Product, bool>> productIdExpression = p => productId.HasValue ? p.ProductId == productId.Value : true;
            Expression<Func<Product, bool>> productNameExpression = p => !string.IsNullOrEmpty(productName) ? p.Name.ToLower().Contains(productName.ToLower()) : true;
            Criteria = productIdExpression.And(productNameExpression);
        }
    }
}
