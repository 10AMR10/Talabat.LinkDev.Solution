using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.service.contract;

namespace Talabat.APIs.Controllers
{
	
	public class PaymentsController : BaseController
	{
		private readonly IPaymentService _paymentService;
		private readonly IMapper _mapper;

		public PaymentsController(IPaymentService paymentService,IMapper mapper)
        {
			this._paymentService = paymentService;
			this._mapper = mapper;
		}
        // service for PaymentIntent 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket= await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
			if (basket is null)
				return BadRequest(new ApiResponse(400));
			var mapped= _mapper.Map<CustomerBasket, CustomerBasketDto>(basket);
			return Ok(mapped);
		}
	}
}
