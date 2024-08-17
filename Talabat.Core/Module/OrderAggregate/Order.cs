using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.OrderAggregate
{
	public class Order : BaseEntity
	{
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public DeliveryMethod? delivery { get; set; }
        public ICollection<OrderItem> order { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + delivery.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
