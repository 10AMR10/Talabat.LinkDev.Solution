﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.orderAgregrate
{
	public class DeliveryMethod:BaseEntity
	{
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string  DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}