using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dto;
using Talabat.APIs.Error;
using Talabat.APIs.Extensions;
using Talabat.Core.Module.Identity;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManger;
		private readonly SignInManager<ApplicationUser> _signInManger;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;
		public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManger, IAuthService authService, IMapper mapper)
		{
			_userManger = userManger;
			_signInManger = signInManger;
			_authService = authService;
			_mapper = mapper;
		}


		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManger.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			var result = await _signInManger.CheckPasswordSignInAsync(user, model.Passwrod, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			return Ok(new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManger)
			});
		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new ApplicationUser
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split("@")[0],
				PhoneNumber = model.PhoneNumber
			};

			var result = await _userManger.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(new ApiValidationResponse() { Errors = result.Errors.Select(p => p.Description) });
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManger)
			});
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManger.FindByEmailAsync(email);

			return Ok(new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManger)
			});
		}

		[Authorize]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetAddressUser()
		{
			var user = await _userManger.FindByEmailAsync(User);

			return Ok(_mapper.Map<AddressDto>(user?.Address));
		}


	}
}
