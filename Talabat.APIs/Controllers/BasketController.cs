using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dto;
using Talabat.APIs.Error;
using Talabat.Core.Module.Basket;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository, IMapper mapper)
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket =  await _basketRepository.GetBasketAsync(id);
			return Ok(basket is null ? new CustomerBasket(id): basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
			var createOrUpdateBasket = await _basketRepository.UpdateAsync(mappedBasket);
			if (createOrUpdateBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(createOrUpdateBasket);
		}

		[HttpDelete]
		public async Task<bool> DeleteBasket(string id)
		{
			return await _basketRepository.DeleteBasketAsync(id);
		}

	}
}
