using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.APIs.Dtos
{
	public class OrderItemDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductUrl { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}