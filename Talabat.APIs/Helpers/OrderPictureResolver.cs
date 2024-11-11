using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.APIs.Helpers
{
	public class OrderPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderPictureResolver(IConfiguration configuration)
        {
			this._configuration = configuration;
		}
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.Product.ProductUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}{source.Product.ProductUrl}";
			}
			return string.Empty ;
		}
	}
}
