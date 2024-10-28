using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseController
	{
		private readonly IGenericRepositry<Product> _productRepo;

		public ProductController(IGenericRepositry<Product> ProductRepo)
		{
			_productRepo = ProductRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
		{
			var products=await _productRepo.GetAllAsync();
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product=await _productRepo.GetAsync(id);
			if (product is null)
				return NotFound(new { Message = "The Product Not Found", StatusCode = "404" });
			return Ok(product);
		}
	}
}
