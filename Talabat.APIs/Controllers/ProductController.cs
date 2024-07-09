using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dto;
using Talabat.APIs.Helper;
using Talabat.Core.Module;
using Talabat.Core.ProductSpecification;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Products> _productRepo;
		private readonly IMapper _mapper;
		public ProductController(IGenericRepository<Products> productRepo, IMapper mapper)
		{
			_productRepo = productRepo;
			_mapper = mapper;
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _productRepo.GetWithSpecASync(spec);
			if(product is null)
				return NotFound();
			return Ok(_mapper.Map<Products,ProductToReturnDto>(product));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			if(products is null)
				return NotFound();
			return Ok(_mapper.Map<IEnumerable<Products>,IEnumerable<ProductToReturnDto>>(products));
		}
	}
}
