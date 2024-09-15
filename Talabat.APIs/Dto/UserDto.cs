using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dto
{
	public class UserDto
	{
        public string email { get; set; } = null!;
        public string displayName { get; set; } = null!;
        public string token { get; set; } = null!;
    }
}
