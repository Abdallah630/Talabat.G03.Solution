namespace Talabat.APIs.Dto
{
	public class OrderItemDto
	{
		public int id { get; set; }
		public int productId { get; set; }
		public string productName { get; set; }
		public string pictureUrl { get; set; }
		public int quantity { get; set; }
		public decimal price { get; set; }

	}
}
