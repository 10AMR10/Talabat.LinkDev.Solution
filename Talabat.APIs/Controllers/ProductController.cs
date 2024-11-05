using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Products_Specific;
using Talabat.Core.repositry.contract;
using Talabat.Core.specification;
using Talabat.Core.specification.Products_Specific;
using Talabat.Repositry;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseController
	{
		private readonly IGenericRepositry<Product> _productRepo;
		private readonly IMapper _mapper;
		private readonly IGenericRepositry<ProductBrand> _brandRepo;
		private readonly IGenericRepositry<ProductCategory> _categoryRepo;

		public ProductController(IGenericRepositry<Product> ProductRepo, IMapper mapper
			, IGenericRepositry<ProductBrand> brandRepo, IGenericRepositry<ProductCategory> categoryRepo)
		{
			_productRepo = ProductRepo;
			_mapper = mapper;
			_brandRepo = brandRepo;
			_categoryRepo = categoryRepo;
		}
		
		[HttpGet] // the body of get => query string so => [FromQuery]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery] ProductSpecPrams productPrams) // uncle bob say more than 3 prams make class and make the object of these 3 prams
		{
			var spec = new ProductWithBrandAndCategory(productPrams); // get all products 
			var products = await _productRepo.GetAllSpecificAsync(spec);
			var mapped = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
			var countSpec = new ProductCountSpecification(productPrams);
			var CountAfterFilteration = await _productRepo.CountAllAsync(countSpec);
			var productPaginations = new Pagination<ProductToReturnDto>(productPrams.PageSize, productPrams.PageIndex, mapped, CountAfterFilteration);

			return Ok(productPaginations);
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
		[HttpGet("Brand")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetALLBrands()
		{
			var brands=await _brandRepo.GetAllAsync();
			if (brands is null)
				return NotFound(new ApiResponse(404));
			return Ok(brands);
		}
		[HttpGet("Brand/{id}")]
		public async Task<ActionResult<ProductBrand>> GetBrand(int id)
		{
			var brand = await _brandRepo.GetAsync(id);
			if (brand is null)
				return NotFound(new ApiResponse(404));
			return Ok(brand);
		}
		[HttpGet("Category")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetALLCategories()
		{
			var categories = await _categoryRepo.GetAllAsync();
			if (categories is null)
				return NotFound(new ApiResponse(404));
			return Ok(categories);
		}
		[HttpGet("Category/{id}")]
		public async Task<ActionResult<ProductCategory>> GetCategories(int id)
		{
			var categories = await _categoryRepo.GetAsync(id);
			if (categories is null)
				return NotFound(new ApiResponse(404));
			return Ok(categories);
		}
	}
}
