using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.SpecificationTest
{
	public class prodctWithBrandWithCategoryTest : BaseSpecificationTest<Product>
	{
	// done
		public prodctWithBrandWithCategoryTest(string sort) : base()
		{
			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "price":
						AddOrderBy( p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(p => p.Price);
						break;
				}
			}
			OrderBy = p => p.Name;
			AddIncludes();
		}


		public prodctWithBrandWithCategoryTest(int id) : base(p => p.Id == id)

		{
			AddIncludes();
		}
		private void AddIncludes()
		{
			Includes.Add(p => p.BrandId);
			Includes.Add(p => p.CategoryId);
		}

	}
}
