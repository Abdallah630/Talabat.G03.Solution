using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.OrderAggregate
{
	public class Order : BaseEntity
	{
        public Order()
        {
            
        }
        public Order(Address shippingAddress, int? deliveryMethodId, ICollection<OrderItem> order, decimal subTotal, string buyerEmail,string paymentIntentId)
		{
			ShippingAddress = shippingAddress;
			DeliveryMethodId = deliveryMethodId;
			OrderItem = order;
			SubTotal = subTotal;
			BuyerEmail = buyerEmail;
            PaymentIntentId = paymentIntentId;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } 
    }
}
