using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.SpecificationTest;

namespace Talabat.Repositry
{
	internal class SpecificEvaluatorTest<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> input,ISpecificationTest<T> spec)
		{
			var query = input;
			if(spec.Criteria is not null)
				query=query.Where(spec.Criteria);
			
			query = spec.Includes.Aggregate(query, (a, b) => a.Include(b));
			//1. storeContext.Set<Product>()
			//2. storeContext.Set<Product>().include(1)
			//3. storeContext.Set<Product>().include(1).include(2)

			return query;
		}
	}
}
