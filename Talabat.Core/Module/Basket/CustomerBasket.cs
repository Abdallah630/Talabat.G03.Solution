﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.Basket
{
	public class CustomerBasket
	{
        public string Id { get; set; }
        public List<BasketItem> Item { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
            Item = new List<BasketItem>();
        }
    }
}
