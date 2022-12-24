using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.core.Specification;
using Talabat_APIs.Dtos;

namespace Talabat_APIs.Controllers
{
    public class ProductsController : BaseAPIController
    {
        public IGenericRepository<Product> IGenericRepository { get; }
        public IMapper Mapper { get; }

        public ProductsController(IGenericRepository<Product> IGenericRepository,IMapper mapper)
        {
            this.IGenericRepository = IGenericRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var specs = new ProductWithBrandAndTypeSpecification();
            var products = await this.IGenericRepository.GetAllWithSpecsAsync(specs);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecification(id);
            var product = await this.IGenericRepository.GetByIdWithSpecsAsync(specs);
            return Ok(Mapper.Map<Product, ProductDto>(product));
        }
    }
}
