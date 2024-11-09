using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string productName { get; set; }
		[Required]
		public string pictureUrl { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage ="Pric start from 0.1")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "quantity start from 1")]
		public int quantity { get; set; }
	}
}
