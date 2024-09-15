using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dto;
using Talabat.APIs.Error;
using Talabat.Core.Module.Identity;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		

		public OrdersController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var adress = _mapper.Map<AddressDto, Addresses>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, adress);
			if (order is null) return BadRequest(new ApiResponse(400, "Order is Null"));
			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser(string email)
		{
			var orders = await _orderService.GetOrderForUserAsync(email);
			return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
		}
		[HttpGet("{Id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int Id)
		{
			var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderByIdForUserAsync(buyerEmail, Id);
			if (order is null) return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}
		[HttpGet("deliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
		{
			var delivery = await _orderService.GetDeliveryMethodAsync();
			return Ok(delivery);
		}

		
	}
}
