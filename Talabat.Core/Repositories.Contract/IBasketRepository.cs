﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Basket;

namespace Talabat.Core.Repositories.Contract
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string basketId);
		Task<CustomerBasket?> UpdateAsync(CustomerBasket basket);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
