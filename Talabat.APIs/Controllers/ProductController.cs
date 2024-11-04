using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Products_Specific;
using Talabat.Core.repositry.contract;
using Talabat.Core.specification;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseController
	{
		private readonly IGenericRepositry<Product> _productRepo;
		private readonly IMapper _mapper;

		public ProductController(IGenericRepositry<Product> ProductRepo, IMapper mapper)
		{
			_productRepo = ProductRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts()
		{
			var spec = new ProductWithBrandAndCategory(); // get all products 
			var products = await _productRepo.GetAllSpecificAsync(spec);
			return Ok(_mapper.Map<IEnumerable<ProductToReturnDto>>(products));
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategory(id);
			var product = await _productRepo.GetSpecificAsync(spec);
			if (product is null)
				return NotFound(new { Message = "The Product Not Found", StatusCode = "404" });
			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}
	}
}
