using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Module.Identity;

namespace Talabat.APIs.Extensions
{
	public static class UserMangerExtensions
	{
		public static async Task<ApplicationUser?> FindByEmailAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal User)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());
			return user;
		}
	}
}
