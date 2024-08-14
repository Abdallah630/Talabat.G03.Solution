using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;
using Talabat.Core.Specifications;

namespace Talabat.Core.ProductSpecification
{
    public class ProductWithBrandAndCategorySpecification : BaseSpecifications<Products>
	{
		public ProductWithBrandAndCategorySpecification()
			:base()
		{
			includes.Add(p => p.Brand);
			includes.Add(p => p.Category);
		}

		public ProductWithBrandAndCategorySpecification(int id)
			: base(p=>p.Id == id)
		{
			includes.Add(p => p.Brand);
			includes.Add(p => p.Category);

		}
	}
}
