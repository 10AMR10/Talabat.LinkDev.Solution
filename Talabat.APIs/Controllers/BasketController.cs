using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;

namespace Talabat.APIs.Controllers
{

	public class BasketController : BaseController
	{
		private readonly IBasketRepository _basketRepo;

		public BasketController(IBasketRepository basketRepo)
		{
			_basketRepo = basketRepo;
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
		public async Task<ActionResult<CustomerBasket>> UpdateOrCreateBasket(CustomerBasket customerBasket)
		{
			var createdOrUpdated = await _basketRepo.UpdateBasketAsync(customerBasket);
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
