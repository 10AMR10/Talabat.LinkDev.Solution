using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;
using Talabat.Core.specification.Products_Specific;

namespace Talabat.Core.Products_Specific
{
	public class ProductWithBrandAndCategory : BaseSpecification<Product>
	{	
		public ProductWithBrandAndCategory(ProductSpecPrams productPrams) :
			base(p =>
			(string.IsNullOrEmpty(productPrams.Search)) || p.Name.ToLower().Contains(productPrams.Search.ToLower()) &&
				 (!productPrams.BrandId.HasValue || p.BrandId == productPrams.BrandId)   && 
			(!productPrams.CategoryId.HasValue || p.CategoryId == productPrams.CategoryId)
				)
		{
			if (!string.IsNullOrEmpty(productPrams.Sort))
			{
				switch (productPrams.Sort)
				{
					case "price":
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			// pagesize=4 pageIndex=2

			AddPagination((productPrams.PageIndex-1)*productPrams.PageSize,productPrams.PageSize);

			AddIncludes();
		}


		public ProductWithBrandAndCategory(int id) : base(p => p.Id == id)
		{
			AddIncludes();

		}
		private void AddIncludes()
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}
