using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.Core.specification.Order_Specific
{
	public class OrdersSpecification:BaseSpecification<Order>
	{
        public OrdersSpecification(string email):base(o=> o.BuyerEmail==email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderBy(o => o.OrderDate);

        }
        public OrdersSpecification(string email,int id):base(o=> o.BuyerEmail==email&&o.Id==id)
        {
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);
			AddOrderBy(o => o.OrderDate);
		}
    }
}
