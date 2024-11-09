using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;

namespace Talabat.APIs.Controllers
{

	public class BasketController : BaseController
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepo,IMapper mapper)
		{
			_basketRepo = basketRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			if (basket is null)
				return Ok(new CustomerBasket(basketId)); // Note the return statement
			return Ok(basket);
		}

		//update or create
		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateOrCreateBasket(CustomerBasketDto customerBasket)
		{
			var mapped = _mapper.Map<CustomerBasket>(customerBasket);
			var createdOrUpdated = await _basketRepo.UpdateBasketAsync(mapped);
			if (createdOrUpdated is null)
				BadRequest(new ApiResponse(400));
			return Ok(createdOrUpdated);
		}
		//delete 
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string basketId)
		{
			return await _basketRepo.DeleteBasketAsync(basketId);
		}

	}
}
