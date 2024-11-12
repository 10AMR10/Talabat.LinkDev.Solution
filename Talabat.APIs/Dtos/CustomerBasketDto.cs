using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
	public class CustomerBasketDto
	{
		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

		// Parameterless constructor (for deserialization)
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
		public int? DeliveryMethodId { get; set; }


	}
}
