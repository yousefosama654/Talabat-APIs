using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specification
{
    public class ProductSpecificationFilterCount : BaseSpecification<Product>
    {
        public ProductSpecificationFilterCount(ProductsSpecParams productsSpecParams) :base(p=>
            
            (string.IsNullOrEmpty(productsSpecParams.search) ||p.Name.ToLower().Contains(productsSpecParams.search.ToLower()))&&
            (!productsSpecParams.BrandId.HasValue || productsSpecParams.BrandId == p.ProductBrandId) &&
            (!productsSpecParams.TypeId.HasValue || productsSpecParams.TypeId==p.ProductTypeId)
            )
        {
            // This will return 
        }
    }
}
