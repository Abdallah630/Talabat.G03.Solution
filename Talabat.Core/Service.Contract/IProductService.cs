using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;
using Talabat.Core.ProductSpecification;

namespace Talabat.Core.Service.Contract
{
	public interface IProductService
	{
		public Task<IReadOnlyList<Products>> GetProductsAsync(ProductPramSpec productSpec);
		public Task<Products?> GetProductAsync(int productId);
		public Task<IReadOnlyList<ProductBrand>> GetProductBrand();
		public Task<IReadOnlyList<ProductCategory>> GetProductCategoryAsync();
		Task<int> GetCountAsync(ProductPramSpec productSpec);
	
	}
}
