
namespace Talabat.APIs.Error
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
		public string? Message { get; set; } 

        public ApiResponse(int statusCode,string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessage(statusCode);
        }

		private string? DefaultMessage(int statusCode)
		{
			return statusCode switch
			{
				400 => "A bad Request, you have made",
				401 => "Authorized, you are not",
				404 => "Resource was not found",
				500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads career change",
				_ => null ,
			};

		}
	}
}
