using AutoMapper;
using StackExchange.Redis;
using Talabat.APIs.Dto;
using Talabat.Core.Module;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.OrderAggregate;
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
			CreateMap<BasketItemDto, BasketItem>();
			CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<AddressDto, Address>();

		}
	}
}
