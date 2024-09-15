using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using Stripe.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Module;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.OrderSpec;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.PaymentService
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;
		public PaymentService(IConfiguration configuration, IUnitOfWork unitOfWork, IBasketRepository basketRepo)
		{
			_configuration = configuration;
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
		}

		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{

			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
			var basket = await _basketRepo.GetBasketAsync(basketId);
			if (basket is null) return null;
			if(basket.items.Count > 0)
			{
				var productRepo = _unitOfWork.Repository<Products>();
				foreach (var item in basket.items)
				{
					var product = await productRepo.GetAsync(item.Id);
					if(item.Price != product.Price)
						product.Price = item.Price;
				}
			}
			var shippingPrice = 0m;

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
				shippingPrice = deliveryMethod.Cost;
				basket.ShippingPrice = shippingPrice;
			}

			PaymentIntent paymentIntent;
			PaymentIntentService paymentIntentService = new PaymentIntentService();

			if(string.IsNullOrEmpty(basket.PaymentIntentId)) //Create new Payment Intent
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.items.Sum(item => item.Price*100 * item.Quantity) + (long)shippingPrice * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string>() {"card"}
				};

				paymentIntent = await paymentIntentService.CreateAsync(options);
				basket.PaymentIntentId =paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}
			else // Update Existing Payment Intent
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
				};

				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
			}

			await _basketRepo.UpdateAsync(basket);
			return basket;
		}

		public async Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid)
		{
			var orderRepo = _unitOfWork.Repository<Order>();
			var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
			var order = await orderRepo.GetWithSpecASync(spec);
			if (order is null) return null;
			if (isPaid)
				order.Status = OrderStatus.PaymantReceived;
			else
				order.Status = OrderStatus.PaymantFailed;
			orderRepo.Update(order);

			await _unitOfWork.CompleteAsync();
			return order;
		}
	}
}
 