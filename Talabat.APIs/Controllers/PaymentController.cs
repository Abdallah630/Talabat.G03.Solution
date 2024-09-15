using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Error;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		const string whSecret = "whsec_388bfa53975de08141c87a880b900dcee0b6ac534aa2edf06ce0b9d0502d9097";
		private readonly ILogger<PaymentController> _logger;
		public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
		{
			_paymentService = paymentService;
			_logger = logger;
		}
		[Authorize]
		[ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status400BadRequest)]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket is null) return BadRequest(new ApiResponse(400, "An Error with your Basket"));
			return Ok(basket);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], whSecret);
				var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
			Order? order;
				// Handle the event
				switch (stripeEvent.Type)
				{
					case Events.PaymentIntentSucceeded:
						 order = await _paymentService.UpdateOrderStatus(paymentIntent.Id,true);
					_logger.LogInformation("Order is Succeeded {0}", order?.PaymentIntentId);
					_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
						break;
					case Events.PaymentIntentPaymentFailed:
						order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
					_logger.LogInformation("Order is Failed {0}", order?.PaymentIntentId);
					_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
					break;
				}
			return Ok();
		}
	}
}
