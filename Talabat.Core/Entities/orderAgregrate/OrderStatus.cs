﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.orderAgregrate
{
	public enum OrderStatus
	{
		[EnumMember(Value = "Pending")]
		Pending,
		[EnumMember(Value = "PaymentRecived")]
		PaymentRecived,
		[EnumMember(Value = "PaymentFialed")]
		PaymentFialed
	}
}
