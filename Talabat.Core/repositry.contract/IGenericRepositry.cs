using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Core.repositry.contract
{
	public interface IGenericRepositry<T> where T : BaseEntity
	{
		public Task<T?> GetAsync(int id);
		public  Task<IEnumerable<T>> GetAllAsync();
		public Task<T?> GetSpecificAsync(ISpecification<T> spec);
		public Task<IEnumerable<T>> GetAllSpecificAsync(ISpecification<T> spec);
	}
}
