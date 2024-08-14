using System.Net;
using System.Text.Json;
using Talabat.APIs.Error;

namespace Talabat.APIs.MiddleWares
{
	public class ExceptionMiddleWare 
	{
		private RequestDelegate _requstDelegate;
		private ILogger<ExceptionMiddleWare> _logger;
		private IWebHostEnvironment _webHostEnvironment;
		public ExceptionMiddleWare(RequestDelegate requstDelegate, ILogger<ExceptionMiddleWare> logger, IWebHostEnvironment webHostEnvironment)
		{
			_requstDelegate = requstDelegate;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			// Take an Action with th request 
			
			// Go to The Next MiddleWare
			try
			{
				await _requstDelegate.Invoke(httpContext);
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				httpContext.Response.StatusCode = 500;
				var type = "application/json";

				var response = _webHostEnvironment.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					:
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var json = JsonSerializer.Serialize(response);
				await httpContext.Response.WriteAsync(json);
			}

			// Take an action with response
		}
	}
}
