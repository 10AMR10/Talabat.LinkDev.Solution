
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.Core.service.contract
{
	public interface IOrderServices
	{
		Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrdersForSpecficUser(string buyerEmail);
		Task<Order> GetOrderByIdForSpecficUser(string buyerEmail, int orderId);
	}
}
