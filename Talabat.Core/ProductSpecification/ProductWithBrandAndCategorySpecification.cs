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
		public ProductWithBrandAndCategorySpecification(ProductPramSpec productSpec)
			: base(p =>
			(string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
			&&
			(!productSpec.BrnadId.HasValue || p.BrandId == productSpec.BrnadId.Value)
			&&
			(!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value)
			)
		{
			if (!string.IsNullOrEmpty(productSpec.Sort))
			{
				switch (productSpec.Sort)
				{
					case "priceAsc":
						//OrderBy = p => p.Price;
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						//OrderByDesc = p => p.Price;
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			else OrderBy = p => p.Name;

		
			includes.Add(p => p.Brand);
			includes.Add(p => p.Category);

			//totalProduct = 18 ~ 20
			//pageSize = 5
			//pageIndex = 3
			ApplyPagination((productSpec.pageIndex - 1) * productSpec.PageSize,productSpec.PageSize);
		}

		public ProductWithBrandAndCategorySpecification(int id)
			: base(p=>p.Id == id)
		{
			includes.Add(p => p.Brand);
			includes.Add(p => p.Category);

		}
	}
}
