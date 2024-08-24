using AutoMapper;
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
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(o=>o.DeliveryMethod,s=>s.MapFrom(o=>o.DeliveryMethod.ShortName))
				.ForMember(o=>o.DeliveryMethodCost, s=>s.MapFrom(o=>o.DeliveryMethod.Cost));
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(o=>o.ProductId,p=>p.MapFrom(o=>o.product.ProductId))
				.ForMember(o=>o.ProductName,p=>p.MapFrom(o=>o.product.ProductName))
				.ForMember(o=>o.PictureUrl,p=>p.MapFrom(o=>o.product.PictureUrl));
				

		}
	}
}
