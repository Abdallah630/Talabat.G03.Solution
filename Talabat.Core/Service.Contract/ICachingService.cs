using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Service.Contract
{
	public interface IResponseCacheService
	{
		Task CacheResponseAsync(string Key, object Response, TimeSpan TimeToLive);
		Task<string?> GetCacheResponseAsync(string Key);
	}
}
