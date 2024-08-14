using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dto;
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
		public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
		{
			var basket =  await _basketRepository.GetBasketAsync(basketId);
			return Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateOrCreateBasket(CustomerBasketDto basket)
		{
			var basketDto = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
			var updatedOrCreated = await _basketRepository.UpdateOrCreateAsync(basketDto);
			return Ok(updatedOrCreated);
		}

		[HttpDelete]
		public async Task<bool> DeleteBasket(string basketId)
		{
			return await _basketRepository.DeleteBasketAsync(basketId);
		}

	}
}
