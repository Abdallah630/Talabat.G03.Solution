using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dto;
using Talabat.APIs.Error;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
			if (order is null) return BadRequest(new ApiResponse(400));
			return Ok(_mapper.Map< Order,OrderToReturnDto>(order));
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser(string email)
		{
			var orders = await _orderService.GetOrderForUserAsync(email);
			return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
		}
		[HttpGet("Id")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id, string email)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(id,email);
			if (order is null) return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}
		[HttpGet("DeliveryMethod")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
		{
			var delivery = await _orderService.GetDeliveryMethodAsync();
			return Ok(delivery);
		}

	}
}
