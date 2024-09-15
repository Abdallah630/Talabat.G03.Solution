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
using Talabat.Core.Module.OrderAggregate;
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


		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManger.FindByEmailAsync(model.email);
			if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			var result = await _signInManger.CheckPasswordSignInAsync(user, model.password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			return Ok(new UserDto
			{
				displayName = user.DisplayName,
				email = user.Email,
				token = await _authService.CreateTokenAsync(user, _userManger)
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
				displayName = user.DisplayName,
				email = user.Email,
				token = await _authService.CreateTokenAsync(user, _userManger)
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
				displayName = user.DisplayName,
				email = user.Email,
				token = await _authService.CreateTokenAsync(user, _userManger)
			});
		}

		[Authorize]
		[HttpGet("address")] 
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var user = await _userManger.FindUserByEmailAsync(User);

			return Ok(_mapper.Map<AddressDto>(user.Address));
		}

		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto address)
		{
			var updateAddress = _mapper.Map<AddressDto,Address>(address);
			var user = await _userManger.FindUserByEmailAsync(User);
			updateAddress.Id = user.Address.Id;
			user.Address = updateAddress;
			var result = await _userManger.UpdateAsync(user);
			if (!result.Succeeded) return BadRequest(new ApiValidationResponse() { Errors = result.Errors.Select(e=>e.Description)});
			return Ok(address);
		}
	}
}
