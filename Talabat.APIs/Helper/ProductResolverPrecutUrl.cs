using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.Dto;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;

namespace Talabat.APIs.Helper
{
    public class ProductResolverPrecutUrl : IValueResolver<Products, ProductToReturnDto, string>
	{
		public string Resolve(Products source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{"https://localhost:7258"}/{source.PictureUrl}";
			}

			return string.Empty;
		}
	}
}

