﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Module.Basket;

namespace Talabat.APIs.Dto
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
        [Required]
		public List<BasketItemDto> Items { get; set; }
		public string? PaymentIntentId { get; set; } 
		public string? ClientSecret { get; set; }
		public int? DeliveryMethodId { get; set; } 
		public decimal? ShippingPrice { get; set; }
	}
}
