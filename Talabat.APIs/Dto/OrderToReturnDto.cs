using System.ComponentModel.DataAnnotations;
using Talabat.Core.Module.OrderAggregate;

namespace Talabat.APIs.Dto
{
	public class OrderToReturnDto
	{
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } 
		public Addresses ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
        public string DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal SubTotal { get; set; }
		public string Status { get; set; }
		public decimal Total {  get; set; }
		public string PaymentIntentId { get; set; }
	}
}
