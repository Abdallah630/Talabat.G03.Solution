using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Module.Basket;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories.BasketRepository
{
	public class BasketRepository : IBasketRepository
	{

		private readonly StackExchange.Redis.IDatabase _dataBase;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _dataBase = connection.GetDatabase();
        }
      

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var basket = await _dataBase.StringGetAsync(basketId);
			return basket.IsNullOrEmpty? null: JsonSerializer.Deserialize<CustomerBasket?>(basket);
		}

		public async Task<CustomerBasket?> UpdateOrCreateAsync(CustomerBasket basket)
		{
			var createOrUpdate = await _dataBase.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
			if (!createOrUpdate) return null;
			return await GetBasketAsync(basket.Id);
		}
		public Task<bool> DeleteBasketAsync(string basketId)
			  => _dataBase.KeyDeleteAsync(basketId);

	}
}
