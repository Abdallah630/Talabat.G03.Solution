using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		//[DataType(DataType.Password)] // HashingPassword
		public string Passwrod { get; set; } 
	}
}
