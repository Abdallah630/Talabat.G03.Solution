using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.OrderAggregate
{
	public enum OrderStatus
	{
		[EnumMember(Value = "Pending")]
		Pending,
		[EnumMember(Value = "Payment Received")]
		PaymantReceived,
		[EnumMember(Value = "Payment Failed")]
		PaymantFailed,
	}
}
