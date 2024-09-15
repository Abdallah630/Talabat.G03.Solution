using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Module.OrderAggregate
{
	public class ProductItemOrder : BaseEntity
	{
        private ProductItemOrder()
        {
            
        }
        public ProductItemOrder(int quantity, decimal price, ProductItemOrder product)
		{
			Quantity = quantity;
			Price = price;
			this.product = product;
		}

		public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductItemOrder product { get; set; }

	}
}
