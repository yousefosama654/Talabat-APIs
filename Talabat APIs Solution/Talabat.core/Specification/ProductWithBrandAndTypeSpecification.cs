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
        public ProductWithBrandAndTypeSpecification(ProductsSpecParams productsSpecParams) :base(p=>
            (string.IsNullOrEmpty(productsSpecParams.search) ||p.Name.ToLower().Contains(productsSpecParams.search.ToLower()))&&
            (!productsSpecParams.BrandId.HasValue || productsSpecParams.BrandId == p.ProductBrandId) &&
            (!productsSpecParams.TypeId.HasValue || productsSpecParams.TypeId==p.ProductTypeId)
            )
        {
            this.Includes.Add(p => p.ProductBrand);
            this.Includes.Add(p => p.ProductType);
            // i will skip the pagesize *(pageindex-1)
            int skip = productsSpecParams.PageSize * (productsSpecParams.PageIndex - 1);
            int take = productsSpecParams.PageSize;
            this.ApplyPagination(skip, take);
            if (!string.IsNullOrEmpty(productsSpecParams.sort))
            {
                switch (productsSpecParams.sort)
                {
                    case "price":
                        this.AddOrderByAscending(p => p.Price);
                        break;
                    case "pricedesc":
                        this.AddOrderByDescending(p => p.Price);
                        break;
                    case "name":
                        this.AddOrderByAscending(p => p.Name);
                        break;
                    case "namedesc":
                        this.AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        this.AddOrderByAscending(p => p.Name);
                        break;
                }
            }
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            this.Includes.Add(p => p.ProductBrand);
            this.Includes.Add(p => p.ProductType);
        }

    }
}