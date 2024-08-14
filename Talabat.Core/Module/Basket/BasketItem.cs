﻿namespace Talabat.Core.Module.Basket
{
	public class BasketItem
	{
        public int Id { get; set; }
		public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}