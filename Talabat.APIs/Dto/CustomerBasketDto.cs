using System.ComponentModel.DataAnnotations;
using Talabat.Core.Module.Basket;

namespace Talabat.APIs.Dto
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
		[Required]
		public List<BasketItemDto> Item { get; set; }
	}
}
