using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.SpecificationTest
{
	public class prodctWithBrandWithCategoryTest:BaseSpecificationTest<Product>
	{
        public prodctWithBrandWithCategoryTest():base()
		{
			AddIncludes();
		}


		public prodctWithBrandWithCategoryTest(int id):base(p=> p.Id==id) 
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
