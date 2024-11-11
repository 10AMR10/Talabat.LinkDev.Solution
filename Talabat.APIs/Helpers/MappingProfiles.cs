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




		}
	}
}
