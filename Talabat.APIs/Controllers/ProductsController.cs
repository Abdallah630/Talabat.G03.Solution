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
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{

		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductsController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		//[Authorize]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductPramSpec productPram)
		{
			var products = await _productService.GetProductsAsync(productPram);
			var count = await _productService.GetCountAsync(productPram);
			var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(products);
			return Ok(new Pagination<ProductToReturnDto>(productPram.PageSize,productPram.pageIndex,count,data));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _productService.GetProductAsync(id);
			if (product is null)
				return NotFound();
			return Ok(_mapper.Map<Products, ProductToReturnDto>(product));
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brand = await _productService.GetProductBrand();
			return Ok(brand);
		}

		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var category = await _productService.GetProductCategoryAsync();
			return Ok(category);
		}
	}
}