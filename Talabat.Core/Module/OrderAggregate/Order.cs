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
        public Order(Address shippingAddress, int? deliveryMethodId, ICollection<OrderItem> order, decimal subTotal, string buyerEmail)
		{
			ShippingAddress = shippingAddress;
			DeliveryMethodId = deliveryMethodId;
			this.order = order;
			SubTotal = subTotal;
			BuyerEmail = buyerEmail;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? delivery { get; set; }
        public ICollection<OrderItem> order { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + delivery.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
