using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContex _storeContex;
		private Hashtable _repositories; // like Dictonary
        public UnitOfWork(StoreContex storeContex)
        {
            _repositories= new Hashtable();
			_storeContex = storeContex;
		}

		public IGenericRepositry<T> GetRepositry<T>() where T : BaseEntity
		{
			var type = typeof(T).Name;
			if (!_repositories.ContainsKey(type))
			{
				_repositories.Add(type, new GenericRepositry<T>(_storeContex));
			}
			return _repositories[type] as IGenericRepositry<T>;	
		}
        public async Task<int> CompleteAsync()
			=> await _storeContex.SaveChangesAsync();

		public async ValueTask DisposeAsync()
			=> await _storeContex .DisposeAsync();
	}
}
