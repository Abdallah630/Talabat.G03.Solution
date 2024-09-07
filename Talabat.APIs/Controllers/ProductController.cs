using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dto;
using Talabat.APIs.Helper;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;
using Talabat.Core.ProductSpecification;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Products> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;
		private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;
		public ProductController(IGenericRepository<Products> productRepo, IMapper mapper, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductCategory> categoryRepo)
		{
			_productRepo = productRepo;
			_mapper = mapper;
			_brandRepo = brandRepo;
			_categoryRepo = categoryRepo;
		}



		//[Authorize]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductPramSpec productPram)
		{
			var spec = new ProductWithBrandAndCategorySpecification(productPram);
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(products);
			var countSpec = new ProductsWithFiltrationForCountSpecifications(productPram);
			var count = await _productRepo.GetCountAsync(countSpec);
			return Ok(new Pagination<ProductToReturnDto>(productPram.PageSize,productPram.pageIndex,count,data));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _productRepo.GetWithSpecASync(spec);
			if (product is null)
				return NotFound();
			return Ok(_mapper.Map<Products, ProductToReturnDto>(product));
		}
		[HttpGet("Brand")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
		{
			var brand = await _brandRepo.GetAllAsync();
			return Ok(brand);
		}

		[HttpGet("Category")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategory()
		{
			var category = await _categoryRepo.GetAllAsync();
			return Ok(category);
		}
	}
}