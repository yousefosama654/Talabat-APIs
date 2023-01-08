using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat_APIs.Dtos;

namespace Talabat_APIs.Controllers
{


    public class BasketController : BaseAPIController
    {
        public BasketController(IBasketRepository IBasketRepository,IMapper mapper)
        {
            this.IBasketRepository = IBasketRepository;
            Mapper = mapper;
        }

        public IBasketRepository IBasketRepository { get; }
        public IMapper Mapper { get; }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketById(string Id)
        {
            var obj= await this.IBasketRepository.DeleteBasketsync(Id);
            return Ok(obj);
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string Id)
        {
            var obj = await this.IBasketRepository.GetBasketByIdAsync(Id);
            // in case there is no busket with this Key create one not in the first time
            return Ok(obj ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto CustomerBasket )
        {
            var mmappedBasket = this.Mapper.Map<CustomerBasketDto, CustomerBasket>(CustomerBasket);
            //create the basket for the first time or update exisiting basket 
            var createOrUpdated = await this.IBasketRepository.UpdateBasketAsync(mmappedBasket);
            return Ok(createOrUpdated);
        }
    }
}
