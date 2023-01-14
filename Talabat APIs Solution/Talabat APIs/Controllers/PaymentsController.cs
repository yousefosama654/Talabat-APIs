using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Servicecs;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{

    public class PaymentsController : BaseAPIController
    {
        public IPaymentService PaymentService { get; }
        public ILogger<PaymentsController> Logger { get; }
        public IConfiguration Configuration { get; }

        const string WebhookSecret = "whsec_9500fe18789581368460b5a89275e6840763e5c359cbc0dc0efdd8a681374410";
        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger, IConfiguration configuration)
        {
            PaymentService = paymentService;
            Logger = logger;
            Configuration = configuration;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await this.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            this.Configuration["CurrentBasketID:value"] = basketId;
            if (basket == null)
            {
                return BadRequest(new ApiResponse(400, "An Error Occured during creating or updating Payment Intent to this basket"));
            }
            else
            {
                return Ok(basket);
            }
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], WebhookSecret);
            PaymentIntent paymentIntent;
            Order order;
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await this.PaymentService.UpdatePayementIntentSucceededOrFailed(paymentIntent.Id, true, this.Configuration["CurrentBasketID:value"]);
                    this.Logger.LogInformation($"Order with id {order.Id} is succeded with intent {paymentIntent.Id}");
                    break;
                case Events.PaymentIntentPaymentFailed:
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await this.PaymentService.UpdatePayementIntentSucceededOrFailed(paymentIntent.Id, false, this.Configuration["CurrentBasketID:value"]);
                    this.Logger.LogInformation($"Order with id {order.Id} is failed with intent {paymentIntent.Id}");
                    break;
            }
            return new EmptyResult();
        }
    }
}
