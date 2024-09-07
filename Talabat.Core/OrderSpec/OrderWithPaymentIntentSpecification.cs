using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Specifications;

namespace Talabat.Core.OrderSpec
{
	public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order>
	{
        public OrderWithPaymentIntentSpecification(string? paymentIntentId)
            :base(o=>o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
