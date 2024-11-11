using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.orderAgregrate;
using Talabat.Core.service.contract;

namespace Talabat.APIs.Controllers
{

	public class OrdersController : BaseController
	{
		private readonly IOrderServices _orderServices;
		private readonly IMapper _mapper;

		public OrdersController(IOrderServices orderServices,IMapper mapper)
		{
			_orderServices = orderServices;
			_mapper = mapper;
		}
		// create order
		[ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDto inputOrder)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var address= _mapper.Map<AddressDto,Address>(inputOrder.shippingAddress);
			var order=await _orderServices.CreateOrderAsync(email,inputOrder.basketId,inputOrder.deliveryId,address);
			if (order is null)
				return BadRequest(new ApiResponse(400, "Can't Create Order"));
			return Ok(order);
		}
		//get order form user email
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>),StatusCodes.Status200OK)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var orders= await _orderServices.GetOrdersForSpecficUserAsync(email);
			if (orders is null) return NotFound(new ApiResponse(400));
			var mapped= _mapper.Map< IReadOnlyList < Order >, IReadOnlyList<OrderToReturnDto>>(orders);
			return Ok(mapped);
		}
		//get order by id 
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderServices.GetOrderByIdForSpecficUserAsync(email,id);
			if (order is null) return NotFound(new ApiResponse(400));
			var mapped = _mapper.Map<Order, OrderToReturnDto>(order);

			return Ok(mapped);
		}


	}
}
