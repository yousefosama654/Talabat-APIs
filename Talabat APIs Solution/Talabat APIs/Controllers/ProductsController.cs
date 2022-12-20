using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;

namespace Talabat_APIs.Controllers
{
    public class ProductsController : BaseAPIController
    {
        public IGenericRepository<Product> IGenericRepository { get; }
        public ProductsController(IGenericRepository<Product> IGenericRepository)
        {
            this.IGenericRepository = IGenericRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await this.IGenericRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await this.IGenericRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
