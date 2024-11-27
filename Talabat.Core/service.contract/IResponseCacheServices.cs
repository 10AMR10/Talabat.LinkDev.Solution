using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.service.contract
{
	public interface IResponseCacheServices
	{
		public Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireTime);
		public Task<string?> GetCachedResponseAsync(string CacheKey);

	}
}
