using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
	public class ProductToReturnDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		
		public string Brand { get; set; }
		//[ForeignKey(nameof(Product.Brand))]  we will make it by fluent api
		public int BrandId { get; set; }

		//create navigaitonal propery (product has one category,category has many products)
		//[InverseProperty(nameof(ProductCategory.Products))] we don't have products so we will make it by fluent api

		public string  Category { get; set; }
		/*[ForeignKey(nameof(Product.Brand))]*/ // we will make it by fluent api
		public int CategoryId { get; set; }
	}
}
