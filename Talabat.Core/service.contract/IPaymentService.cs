using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.service.contract
{
	public interface IPaymentService
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketId);
	}
}
