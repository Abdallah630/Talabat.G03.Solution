using AutoMapper;
using Talabat.APIs.Dto;
using Talabat.Core.Module;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.Identity;
using Talabat.Core.Module.Product;

namespace Talabat.APIs.Helper
{
    public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Products, ProductToReturnDto>()
				.ForMember(p => p.Brand,
				   p => p.MapFrom(p => p.Name))
				.ForMember(p => p.Category, p => p.MapFrom(p => p.Name))
				.ForMember(p => p.PictureUrl, p => p.MapFrom<ProductResolverPrecutUrl>());
			CreateMap<BasketItem, BasketItemDto>();
			CreateMap<CustomerBasket, CustomerBasketDto>();
			CreateMap<Address, AddressDto>();

		}
	}
}
