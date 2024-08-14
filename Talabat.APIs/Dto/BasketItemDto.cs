using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class BasketItemDto
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage = "Price must by greater than zero.")]
		public decimal Price { get; set; }
		[Required]
		[Range(1,int.MaxValue,ErrorMessage= "Quantity must by greater than zero.")]
		public int Quantity { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		public string Brand { get; set; }
	}
}