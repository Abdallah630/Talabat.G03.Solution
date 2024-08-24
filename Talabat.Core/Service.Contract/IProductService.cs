using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;

namespace Talabat.Core.Service.Contract
{
	public interface IProductService
	{
		public Task<IReadOnlyList<Products>> GetProductsAsync();
		public Task<Products> GetProductAsync(int productId);
		public Task<ProductBrand> GetProductBrand();
		public Task<ProductCategory> GetProductCategoryAsync();
    }
}
