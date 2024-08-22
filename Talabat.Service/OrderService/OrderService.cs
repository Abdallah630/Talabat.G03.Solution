using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Module;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
		}

		public async Task<Order> GetOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			// 1.Get Basket From Baskets Repo
			var basket = await _basketRepo.GetBasketAsync(basketId);
			// 2. Get Selected Items at Basket From Products Repo
			var orderItems = new List<OrderItem>();
			if(basket?.Item?.Count > 0)
			{
				foreach(var item in orderItems)
				{
					var product = await _unitOfWork.Repository<Products>().GetAsync(item.Id);
					var productItemOrder = new ProductItemOrder(product.Id,product.Name,product.PictureUrl);
					var orderItem = new OrderItem(item.Quantity,product.Price, productItemOrder);
					orderItems.Add(orderItem);
				}
			}
			// 3. Calculate SubTotal
			var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
			// 4. Get Delivery Method From DeliveryMethods Repo
			var deliveryMethod = _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
			// 5. Create Order
			var order = new Order(
				shippingAddress: shippingAddress,
				deliveryMethodId: deliveryMethodId,
				order: orderItems,
				subTotal: subTotal,
				buyerEmail: buyerEmail
				);
			 _unitOfWork.Repository<Order>().Add(order);
			// 6. Save To Database [TODO]
			var result = await _unitOfWork.CompleteAsync();
			if (result <= 0) return null;
			return order;
		

			
		}
		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
		{
			throw new NotImplementedException();
		}


		public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
