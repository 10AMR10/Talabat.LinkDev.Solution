﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.orderAgregrate
{
	public class Order:BaseEntity
	{
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus Status { get; set; }= OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        //navigation property 
        //public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal Total()
            => SubTotal+ DeliveryMethod.Cost    ;
        public string PaymentIntentId { get; set; }= string.Empty;

    }
}
