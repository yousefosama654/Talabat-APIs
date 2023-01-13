using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Servicecs;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{
    [Authorize]
    public class PaymentsController : BaseAPIController
    {
        public IPaymentService PaymentService { get; }
        public PaymentsController(IPaymentService paymentService)
        {
            PaymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await this.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null)
            {
                return BadRequest(new ApiResponse(400, "An Error Occured during creating or updating Payment Intent to this basket"));
            }
            else
            {
                return Ok(basket);
            }
        }
    }
}
