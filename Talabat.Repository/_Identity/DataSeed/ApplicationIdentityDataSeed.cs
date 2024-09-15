using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Identity;

namespace Talabat.Repository._Identity.DataSeed
{
	public static class ApplicationIdentityDataSeed 
	{
		public	static async Task SeedUserAsync(UserManager<ApplicationUser> applicationUser)
		{
			if (!applicationUser.Users.Any())
			{
				var user = new ApplicationUser
				{
					DisplayName = "AbdallahSaad",
					Email = "abdallah@gmail.com",
					UserName = "Abdallah.saad",
					PhoneNumber = "01238745322",
					Address = new Address
					{
						FirstName = "Abdallah",
						LastName = "Saad",
						Street = "213",
						City = "Cairo",
						Country = "Egypt",
					}
				}; 
				await applicationUser.CreateAsync(user,"P@ssw0rd1234");
			}

			

		}


	}
}
