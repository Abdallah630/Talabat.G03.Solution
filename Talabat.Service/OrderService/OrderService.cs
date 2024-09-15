using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Module;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.OrderSpec;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo, IPaymentService paymentService)
		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
			_paymentService = paymentService;
		}

		public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Addresses shippingAddress)
		{
			// 1.Get Basket From Baskets Repo
			var basket = await _basketRepo.GetBasketAsync(basketId);
			// 2. Get Selected Items at Basket From Products Repo
			var orderItems = new List<ProductItemOrder>();
			if(basket?.items?.Count > 0)
			{
				foreach(var item in basket.items)
				{
					var product = await _unitOfWork.Repository<Products>().GetAsync(item.Id);
					var productItemOrder = new ProductItemOrder(item.Id,product.Name,product.PictureUrl);
					var orderItem = new ProductItemOrder(item.Quantity,product.Price, productItemOrder);
					orderItems.Add(orderItem);
				}
			}
			// 3. Calculate SubTotal
			var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
			// 4. Get Delivery Method From DeliveryMethods Repo
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
			if (deliveryMethod is null) return null;

			var orderRepo = _unitOfWork.Repository<Order>();
			var spec = new OrderWithPaymentIntentSpecification(basket?.PaymentIntentId);
			var existingOrder = await orderRepo.GetWithSpecASync(spec);
			if(existingOrder is not null)
			{
				orderRepo.Delete(existingOrder);
				await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			}
			// 5. Create Order
			var order = new Order(buyerEmail, shippingAddress,deliveryMethodId,orderItems,subTotal,basket.PaymentIntentId);
			  orderRepo.AddAsync(order);
			// 6. Save To Database [TODO]
			var result = await _unitOfWork.CompleteAsync();
			if (result <= 0) return null;
			return order;

				

		}
		public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
		{
			var orderRepo = _unitOfWork.Repository<Order>();
			var spec = new OrderSpecification(buyerEmail);
			var order = await orderRepo.GetAllWithSpecAsync(spec);

			return order;
		}
		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
		{
			return (IReadOnlyList<DeliveryMethod>)await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
		}


		public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail,int orderId)
		{
			var orderRepo = _unitOfWork.Repository<Order>();
			var spec = new OrderSpecification(orderId,buyerEmail);
			var order = await orderRepo.GetWithSpecASync(spec);
			return order;
		}
	}
}
