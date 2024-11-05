using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;
using Talabat.Core.SpecificationTest;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductTestController : ControllerBase
	{
		private readonly IGenericRepositry<Product> _productRepo;
		private readonly IMapper _mapper;
		private readonly IGenericRepositry<ProductBrand> _brandRepo;
		private readonly IGenericRepositry<ProductCategory> _categoryRepo;

		public ProductTestController(IGenericRepositry<Product> ProductRepo, IMapper mapper
			, IGenericRepositry<ProductBrand> brandRepo, IGenericRepositry<ProductCategory> categoryRepo)
		{
			_productRepo = ProductRepo;
			_mapper = mapper;
			_brandRepo = brandRepo;
			_categoryRepo = categoryRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort)
		{
			var productSped = new prodctWithBrandWithCategoryTest(sort);
			var products = await _productRepo.GetAllSpecificAsyncTest(productSped);
			var mapped = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
			return Ok(mapped);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var productSped = new prodctWithBrandWithCategoryTest(id);
			var product = await _productRepo.GetSpecificAsyncTest(productSped);
			var mapped = _mapper.Map<Product, ProductToReturnDto>(product);
			return Ok(mapped);
		 }

	}
}
