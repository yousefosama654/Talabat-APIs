using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
namespace Talabat.core.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        // we will add the includes of related data of Product here 
        public ProductWithBrandAndTypeSpecification() : base()
        {
            this.Includes.Add(p => p.ProductBrand);
            this.Includes.Add(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            this.Includes.Add(p => p.ProductBrand);
            this.Includes.Add(p => p.ProductType);
        }
    }
}