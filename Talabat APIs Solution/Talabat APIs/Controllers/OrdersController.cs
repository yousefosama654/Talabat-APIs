using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Servicecs;
using Talabat.Repository.Data.Migrations;
using Talabat_APIs.Dtos;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        public IOrderService IOrderService { get; }
        public IMapper Mapper { get; }

        public OrdersController(IOrderService IOrderService, IMapper mapper)
        {
            this.IOrderService = IOrderService;
            Mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Talabat.core.Entities.Order_Aggregate.Order>> CreateOrder(OrderDto OrderDto)
        {
            var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
            var address = this.Mapper.Map<AddressDto, Address>(OrderDto.shippingaddress);

            var order = await this.IOrderService.CreateOrder(userEmail, OrderDto.basketId, address, OrderDto.deliveryMethodId);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "An Error Occured in Creating New Order"));
            }
        }
    }
}
