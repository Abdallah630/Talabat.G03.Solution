using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Specifications;

namespace Talabat.Core.ProductSpecification
{
	public class ProductsWithFiltrationForCountSpecifications : BaseSpecifications<Products>
	{
		public ProductsWithFiltrationForCountSpecifications(ProductPramSpec productSpec)
			:base(p =>
			(!productSpec.BrnadId.HasValue || p.BrandId == productSpec.BrnadId.Value)
			&&
			(!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value)
			)
		{
		}
	}
}
