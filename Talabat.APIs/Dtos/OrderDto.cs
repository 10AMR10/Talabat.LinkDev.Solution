using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
	public class OrderDto
	{
        [Required]
        public string basketId { get; set; }
		[Required]
		public int deliveryId { get; set; }
		[Required]
		public AddressDto shippingAddress { get; set; }
    }
}
