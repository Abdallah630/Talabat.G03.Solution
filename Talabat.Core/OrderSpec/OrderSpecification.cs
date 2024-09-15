using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.OrderAggregate;
using Talabat.Core.Specifications;

namespace Talabat.Core.OrderSpec
{
	public class OrderSpecification : BaseSpecifications<Order>
	{
        public OrderSpecification(string buyerEmail)
            :base(o=>o.BuyerEmail == buyerEmail) 
        {
            includes.Add(o => o.DeliveryMethod);
            includes.Add(o => o.Items);
            
        }

        public OrderSpecification(int id, string buyerEmail) 
            : base(o=>o.Id == id && o.BuyerEmail == buyerEmail)
        {
            includes.Add(o => o.DeliveryMethod);
            includes.Add(o => o.Items);
        }
    }
}
