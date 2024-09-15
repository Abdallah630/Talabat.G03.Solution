using AutoMapper;
using Talabat.APIs.Dto;
using Talabat.Core.Module;
using Talabat.Core.Module.Basket;
using Talabat.Core.Module.Identity;
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
			CreateMap<Address, AddressDto>().ReverseMap();
			CreateMap<AddressDto, Addresses>();
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(o=>o.DeliveryMethod,s=>s.MapFrom(o=>o.DeliveryMethod.ShortName))
				.ForMember(o=>o.DeliveryMethodCost, s=>s.MapFrom(o=>o.DeliveryMethod.Cost));
			CreateMap<ProductItemOrder, OrderItemDto>()
				.ForMember(o => o.productId, p => p.MapFrom(o => o.product.ProductId))
				.ForMember(o => o.productName, p => p.MapFrom(o => o.product.ProductName))
				.ForMember(o => o.pictureUrl, p => p.MapFrom(o => o.product.PictureUrl));
				

		}
	}
}
