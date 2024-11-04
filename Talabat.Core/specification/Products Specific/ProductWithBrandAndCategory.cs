using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Core.Products_Specific
{
	public class ProductWithBrandAndCategory : BaseSpecification<Product>
	{
		public ProductWithBrandAndCategory(string sort) : base()
		{
			switch (sort)
			{
				case "price":
					OrderBy = p => p.Price;
					break;
				case "priceDesc":
					OrderByDesc = p => p.Price;
					break;
				default:
					OrderBy = p => p.Name;
					break;
			}

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
