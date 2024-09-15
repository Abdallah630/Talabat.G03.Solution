using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Identity;

namespace Talabat.Core.Module.OrderAggregate
{
	public class Order : BaseEntity
	{
        public Order()
        {
            
        }
        public Order(string buyerEmail, Addresses shippingAddress, int? deliveryMethodId, ICollection<ProductItemOrder> order, decimal subTotal, string paymentIntentId)
		{
			ShippingAddress = shippingAddress;
			DeliveryMethodId = deliveryMethodId;
			Items = order;
			SubTotal = subTotal;
			BuyerEmail = buyerEmail;
            PaymentIntentId = paymentIntentId;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Addresses ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public ICollection<ProductItemOrder> Items { get; set; } = new HashSet<ProductItemOrder>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } 
    }
}
