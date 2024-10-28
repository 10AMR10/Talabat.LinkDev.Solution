using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.repositry.contract
{
	public interface IGenericRepositry<T> where T : BaseEntity
	{
		public Task<T?> GetAsync(int id);
		public  Task<IEnumerable<T>> GetAllAsync();
	}
}
