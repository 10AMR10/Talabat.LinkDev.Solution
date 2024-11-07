using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.repositry.contract;

namespace Talabat.Repositry
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer Redis)
		{
			_database = Redis.GetDatabase();
		}
		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var BasketJson= await _database.StringGetAsync(basketId);
			if(BasketJson.IsNull)
				return null;
			return  JsonSerializer.Deserialize<CustomerBasket>(BasketJson);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var basketJson=JsonSerializer.Serialize(basket);
			var CreatedOrUpdated=await _database.StringSetAsync(basket.Id, basketJson,TimeSpan.FromDays(2));
			if (!CreatedOrUpdated)
				return null;
			return await GetBasketAsync(basket.Id);
		}
	}
}
