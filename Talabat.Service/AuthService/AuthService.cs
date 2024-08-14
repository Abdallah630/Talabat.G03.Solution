using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Identity;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
		{
			var authClaim = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,user.DisplayName),
				new Claim(ClaimTypes.Email,user.Email),
			};

			var userRole = await userManager.GetRolesAsync(user);
			foreach (var role in userRole)
			{
				authClaim.Add(new Claim(ClaimTypes.Role, role));
			}

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]));

			//Payload
			var token = new JwtSecurityToken(
				audience: _configuration["JWT:ValidAudience"],
				issuer: _configuration["JWT:ValidIssuer"],
				expires : DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
				claims: authClaim,
				signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
