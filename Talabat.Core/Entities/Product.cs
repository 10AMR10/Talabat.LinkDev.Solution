using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		//create navigaitonal propery (product has one brand,brand has many products) <summary>
		//[InverseProperty(nameof(ProductCategory.Products))] we don't have products so we will make it by fluent api
		public ProductBrand Brand { get; set; }
		//[ForeignKey(nameof(Product.Brand))]  we will make it by fluent api
		public string BrandId { get; set; }

		//create navigaitonal propery (product has one category,category has many products)
		//[InverseProperty(nameof(ProductCategory.Products))] we don't have products so we will make it by fluent api

		public ProductCategory Category { get; set; }
		/*[ForeignKey(nameof(Product.Brand))]*/ // we will make it by fluent api
		public string CategoryId { get; set; }
    }
}
