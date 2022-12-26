using AutoMapper;
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
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductBrand> BrandRepository { get; }
        public IGenericRepository<ProductType> TypeRepository { get; }
        public IMapper Mapper { get; }
        public ProductsController(IGenericRepository<Product> ProductRepository,
            IGenericRepository<ProductBrand> BrandRepository,
            IGenericRepository<ProductType> TypeRepository,
            IMapper mapper)
        {
            this.ProductRepository = ProductRepository;
            this.BrandRepository = BrandRepository;
            this.TypeRepository = TypeRepository;
            Mapper = mapper;
        }
        [HttpGet]
        // the paramter will be a model state or query string
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductsSpecParams productsSpecParams)
        {
            var specs = new ProductWithBrandAndTypeSpecification(productsSpecParams);
            var products = await this.ProductRepository.GetAllWithSpecsAsync(specs);
            var data = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var countSpecs = new ProductSpecificationFilterCount(productsSpecParams);
            var count= await this.ProductRepository.GetCountAsync(countSpecs);
            var PaginatedData = new Pagination<ProductDto>(data, productsSpecParams.PageSize, productsSpecParams.PageIndex, count);
            return Ok(PaginatedData);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecification(id);
            var product = await this.ProductRepository.GetByIdWithSpecsAsync(specs);

            return new OkObjectResult(Mapper.Map<Product, ProductDto>(product));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
        {
            var ProductBrands = await this.BrandRepository.GetAllAsync();
            return new OkObjectResult(ProductBrands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
        {
            var ProductTypes = await this.TypeRepository.GetAllAsync();
            return new OkObjectResult(ProductTypes);
        }
    }
}
