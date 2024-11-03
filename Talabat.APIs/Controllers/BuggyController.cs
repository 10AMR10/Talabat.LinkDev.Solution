using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Repositry.Data;

namespace Talabat.APIs.Controllers
{
	
	public class BuggyController : BaseController
	{
		private readonly StoreContex _storeContex;

		public BuggyController(StoreContex storeContex)
        {
			_storeContex = storeContex;
		}
        [HttpGet("notFound/{id}")]
		public ActionResult<Product> GetNotFound(int id)
		{
			var p = _storeContex.products.Find(id);
			if(p is null) return NotFound(new ApiResponse(404));
			return Ok(p);
		}
		[HttpGet("serverError")]
		public ActionResult<Product> GetServerError()
		{
			var p = _storeContex.products.Find(100).ToString();
			
			return Ok(p);
		}
		[HttpGet("badRequst")]
		public ActionResult<Product> GetNotBadRequst()
		{
			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("badRequst/{id}")]
		public ActionResult<Product> GetBadRequest(int id)
		{
			var p = _storeContex.products.Find(100);
			if (p is null) return NotFound();
			return Ok(p);
		}
	}
}
