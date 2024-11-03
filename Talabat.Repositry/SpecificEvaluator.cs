using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Repositry
{
	internal static class SpecificEvaluator<TE> where TE:BaseEntity
	{
		public static IQueryable<TE> GetQuery(IQueryable<TE> inputSeq,ISpecification<TE> spec)
		{
			var query = inputSeq; // _storeContext.Set<Product>()
			if (spec.Criteria is not null)
				query= query.Where(spec.Criteria); //_storeContext.Set<Product>().Where(x => x.Id == id)
			// using Aggregat function to apply the list of include
			// List<Includes> = {x=>x.Brand, x=>x.Category}
			 query = spec.Includes.Aggregate(query,(currentQuery,includeExpression)=> currentQuery.Include(includeExpression));
			// 1. _storeContext.Set<Product>().Where(x => x.Id == id).Include(x=> x.Brand)
			// 2. _storeContext.Set<Product>().Where(x => x.Id == id).Include(x=> x.Brand).Include(x=> x.Category)

			return query;
		}
	}
}
