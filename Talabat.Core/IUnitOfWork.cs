using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;

namespace Talabat.Core
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		Task<int> CompleteAsync();
		IGenericRepositry<T> GetRepositry<T>() where T : BaseEntity;
	}
}
