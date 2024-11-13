using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.orderAgregrate
{
	public class OrderItem:BaseEntity
	{
        public OrderItem()
        {
            
        }
        public OrderItem( ProductOrderItem product, int quantity, decimal price)
		{
			
			Product = product;
			Quantity = quantity;
			Price = price;
		}

		public int Id { get; set; }
        public ProductOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
