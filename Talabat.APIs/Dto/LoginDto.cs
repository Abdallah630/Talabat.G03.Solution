using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public string email { get; set; }
		[Required]
		//[DataType(DataType.Password)] // HashingPassword
		public string password { get; set; } 
	}
}
