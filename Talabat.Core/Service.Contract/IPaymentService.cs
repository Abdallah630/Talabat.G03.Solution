using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.OrderAggregate;

namespace Talabat.Core.Service.Contract
{
	public interface IPaymentService
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
		Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);
	}
}
