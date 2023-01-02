using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;

namespace Talabat_APIs.Controllers
{


    public class BasketController : BaseAPIController
    {
        public BasketController(IBasketRepository IBasketRepository)
        {
            this.IBasketRepository = IBasketRepository;
        }

        public IBasketRepository IBasketRepository { get; }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasketById(string Id)
        {
            var obj= await this.IBasketRepository.DeleteBasketsync(Id);
            return Ok(obj);
        }
        [HttpGet]
        public async Task<ActionResult> GetBasketById(string Id)
        {
            var obj = await this.IBasketRepository.GetBasketByIdAsync(Id);
            // in case there is no busket with this Key create one not in the first time
            return Ok(obj ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateBasket(CustomerBasket CustomerBasket )
        {
            //create the basket for the first time or update exisiting basket 
            var createOrUpdated = await this.IBasketRepository.UpdateBasketAsync(CustomerBasket);
            return Ok(createOrUpdated);
        }
    }
}
