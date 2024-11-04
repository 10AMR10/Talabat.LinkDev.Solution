using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;
using Talabat.Core.specification;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
	public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntity
	{
		private readonly StoreContex _storeContext;

		public GenericRepositry(StoreContex storeContext)
		{
			_storeContext = storeContext;
		}
		public async Task<T?> GetAsync(int id)
		{
			//if (typeof(T) == typeof(Product))
			//	return await _storeContext.Set<Product>().Where(x => x.Id == id).Include(x => x.Brand)
			//		.Include(x => x.Category).FirstOrDefaultAsync()as T;
			return await _storeContext.Set<T>().FindAsync(id);
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			//if (typeof(T) == typeof(Product))
			//	return (IEnumerable<T>)await _storeContext.Set<Product>().Include(x => x.Brand)
			//		.Include(x => x.Category).AsNoTracking().ToListAsync();
			return await _storeContext.Set<T>().AsNoTracking().ToListAsync();
		}


		public async Task<T?> GetSpecificAsync(ISpecification<T> spec)
		{
			return await ApplicationSpecifications(spec).AsNoTracking().FirstOrDefaultAsync();
		}
		public async Task<IEnumerable<T>> GetAllSpecificAsync(ISpecification<T> spec)
		{
			return await ApplicationSpecifications(spec).AsNoTracking().ToListAsync();

		}
		private IQueryable<T> ApplicationSpecifications(ISpecification<T> spec)
		{
			return SpecificEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec);
		}
	}
}
