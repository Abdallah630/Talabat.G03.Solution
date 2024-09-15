using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class BasketItemDto
	{
		public int id { get; set; }
		[Required]
		public string productName { get; set; }															
		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage = "Price must by greater than zero.")]
		public decimal price { get; set; }
		[Required]
		[Range(1,int.MaxValue,ErrorMessage= "Quantity must by greater than zero.")]
		public int quantity { get; set; }
		[Required]
		public string pictureUrl { get; set; }
		[Required]
		public string brand { get; set; }
		[Required]
		public string category { get; set; }
	}
}