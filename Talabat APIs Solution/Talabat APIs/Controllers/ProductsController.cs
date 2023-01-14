using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.core.Specification;
using Talabat_APIs.Dtos;
using Talabat_APIs.Helpers;

namespace Talabat_APIs.Controllers
{
    public class ProductsController : BaseAPIController
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }

        public ProductsController( IMapper mapper,IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
        [CachedAttribute(600)]
        [HttpGet]
        // the paramter will be a model state or query string
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductsSpecParams productsSpecParams)
        {
            var specs = new ProductWithBrandAndTypeSpecification(productsSpecParams);
            var products = await this.UnitOfWork.Repository<Product>().GetAllWithSpecsAsync(specs);
            var data = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var countSpecs = new ProductSpecificationFilterCount(productsSpecParams);
            var count= await this.UnitOfWork.Repository<Product>().GetCountAsync(countSpecs);
            var PaginatedData = new Pagination<ProductDto>(data, productsSpecParams.PageSize, productsSpecParams.PageIndex, count);
            return Ok(PaginatedData);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecification(id);
            var product = await this.UnitOfWork.Repository<Product>().GetByIdWithSpecsAsync(specs);

            return new OkObjectResult(Mapper.Map<Product, ProductDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
        {
            var ProductBrands = await this.UnitOfWork.Repository<ProductBrand>().GetAllAsync();
            return new OkObjectResult(ProductBrands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
        {
            var ProductTypes = await this.UnitOfWork.Repository<ProductType>().GetAllAsync();
            return new OkObjectResult(ProductTypes);
        }
    }
}
