using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.Product;
using Talabat.Core.Module;
using Talabat.Core.ProductSpecification;
using Talabat.Core;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		

		public async Task<IReadOnlyList<Products>> GetProductsAsync(ProductPramSpec productSpec)
		{
			var spec = new ProductWithBrandAndCategorySpecification(productSpec);
			var prodcut = await _unitOfWork.Repository<Products>().GetAllWithSpecAsync(spec);
			return prodcut;
		}

		public async Task<Products?> GetProductAsync(int productId)
		{
			var spec = new ProductWithBrandAndCategorySpecification(productId);
			var product = await _unitOfWork.Repository<Products>().GetWithSpecASync(spec);
			return product;
		}

		public async Task<int> GetCountAsync(ProductPramSpec productSpec)
		{
			var spec = new ProductsWithFiltrationForCountSpecifications(productSpec);
			var count = await _unitOfWork.Repository<Products>().GetCountAsync(spec);
			return count;
		}
		public async Task<IReadOnlyList<ProductBrand>> GetProductBrand()
			=> await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

		public async Task<IReadOnlyList<ProductCategory>> GetProductCategoryAsync()
			=> await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

	
	}
}

