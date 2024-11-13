using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.Core.specification.Order_Specific
{
	public class OrderWithPaymentIntentSpecification:BaseSpecification<Order>
	{
        public OrderWithPaymentIntentSpecification(string paymentIntentId):base(o=> o.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
