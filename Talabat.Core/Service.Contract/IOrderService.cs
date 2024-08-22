using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.OrderAggregate;

namespace Talabat.Core.Service.Contract
{
	public interface IOrderService
	{
		Task<Order?> CreateOrderAsync(string buyerEmail,string basketId, int deliveryMethodId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
		Task<Order> GetOrderByIdForUserAsync(string buyerEmail,int orderId);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
	}
}
