using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.OrderAggregate
{
	public enum OrderStatus
	{
		Pending,
		PaymantReceived,
		PaymantFailed,
	}
}
