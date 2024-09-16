using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.CacheService
{
	public class ResponseCacheService : IResponseCacheService
	{
		//add RedisDb
		private readonly IDatabase _database;

		public ResponseCacheService(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}

		public async Task CacheResponseAsync(string Key, object Response, TimeSpan TimeToLive)
		{
			//Validate Response Is null
			if (Response is null) return;
			//set properties name in json camelCase
			var serializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
			//Convert from object to json
			var serializerResponse = JsonSerializer.Serialize(Response,serializerOptions);
			// Not Null
			await _database.StringSetAsync(Key,serializerResponse,TimeToLive);
			
		}

		public async Task<string?> GetCacheResponseAsync(string Key)
		{
			//Get Response by key
			var response =  await _database.StringGetAsync(Key);
			//Valid Response Is null 
			if(response.IsNullOrEmpty)
				return null;
			//is not null
			return response;
		}
	}
}
