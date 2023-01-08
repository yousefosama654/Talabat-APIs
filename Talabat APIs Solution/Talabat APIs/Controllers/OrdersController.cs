using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto OrderDto)
        {
            var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
            var address = this.Mapper.Map<AddressDto, Address>(OrderDto.shippingaddress);

            var order = await this.IOrderService.CreateOrder(userEmail, OrderDto.basketId, address, OrderDto.deliveryMethodId);
            var mappedOrder = this.Mapper.Map<Talabat.core.Entities.Order_Aggregate.Order, OrderToReturnDto>(order);

            if (order != null)
            {
                return Ok(mappedOrder);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "An Error Occured in Creating New Order"));
            }
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUserAsync()
        {
            var useremail = this.User.FindFirstValue(ClaimTypes.Email);
            var order = await this.IOrderService.GetOrdersForUserAsync(useremail);
            var mappedOrder = this.Mapper.Map<IReadOnlyList<Talabat.core.Entities.Order_Aggregate.Order>, IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(mappedOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var useremail = this.User.FindFirstValue(ClaimTypes.Email);
            var order = await this.IOrderService.GetOrderByIdForUserAsync(useremail, id);
            var mappedOrder = this.Mapper.Map<Talabat.core.Entities.Order_Aggregate.Order,OrderToReturnDto>(order);
            if (order == null)
                return BadRequest(new ApiResponse(400, "No Order With This Id For The Current User"));
            return Ok(mappedOrder);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var useremail = this.User.FindFirstValue(ClaimTypes.Email);
            return Ok(await this.IOrderService.GetDeliveryMethodsAsync(useremail));
        }
    }
}
