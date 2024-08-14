using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class UserDto
	{
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
