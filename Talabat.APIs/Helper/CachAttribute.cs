using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Service.Contract;
using Talabat.Service.CacheService;

namespace Talabat.APIs.Helper
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _timeToLiveInSeconds;

		public CachedAttribute(int timeToLiveInSeconds)
		{
			_timeToLiveInSeconds = timeToLiveInSeconds;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var responseCacheService =  context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
			var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
			var response = await responseCacheService.GetCacheResponseAsync(cacheKey);
			if (!string.IsNullOrEmpty(response)) // is not null
			{
				context.Result = new ContentResult()
				{
					Content = response,
					ContentType = "application/json",
					StatusCode = 200,
				};

				return;
			}

			var executedActionContext = await next.Invoke();
			if(executedActionContext.Result is OkObjectResult okObjectResut && okObjectResut.Value is not null)
			{
				await responseCacheService.CacheResponseAsync(cacheKey,okObjectResut.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
			}
		}

		private string GenerateCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();
			keyBuilder.Append(request.Path);
			foreach (var (key,value) in request.Query)
			{
				keyBuilder.Append($"|{key}-{value}");
			} 
			return keyBuilder.ToString();
		}
	}
}
