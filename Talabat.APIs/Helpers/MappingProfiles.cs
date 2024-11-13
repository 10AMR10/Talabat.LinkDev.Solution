using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
				.ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

			CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
			CreateMap<AddressDto,Core.Entities.orderAgregrate.Address>().ReverseMap();
			CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(d => d.DeliveryMethod, o => o.MapFrom(o => o.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(o => o.DeliveryMethod.Cost))
				.ForMember(d => d.Status, o => o.MapFrom(o => o.Status.ToString()));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductId, o => o.MapFrom(o => o.Product.ProductId))
				.ForMember(d => d.ProductName, o => o.MapFrom(o => o.Product.ProductName))
				.ForMember(d => d.ProductUrl, o => o.MapFrom(o => o.Product.ProductUrl))
				.ForMember(d => d.ProductUrl, o => o.MapFrom<OrderPictureResolver>());









		}
	}
}
