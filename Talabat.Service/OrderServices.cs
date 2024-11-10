using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.orderAgregrate;
using Talabat.Core.repositry.contract;
using Talabat.Core.service.contract;

namespace Talabat.Service
{
	public class OrderServices : IOrderServices // we implent it in service layer because it still doesn't Build
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;

		public OrderServices(IBasketRepository basketRepo, IUnitOfWork unitOfWork) 
		{
			_basketRepo = basketRepo;
			_unitOfWork = unitOfWork;
		}
        public async Task<Order?> CreateOrderAsync(string buyerEmail,	 string basketId, int DeliveryMethodId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId); // i get basket to foul ordreItem

			var orderItems = new List<OrderItem>();
			if (basket?.Items.Count>0)
			{
				foreach (var item in basket.Items)
				{
					var product=await _unitOfWork.GetRepositry<Product>().GetAsync(item.Id);
					var productItem=new ProductOrderItem(product.Id,product.Name,product.PictureUrl);
					var orderItem = new OrderItem(product.Id, productItem, item.quantity, product.Price);
					orderItems.Add(orderItem);
				} 
			}
			var subTotal= orderItems.Sum(x=> x.Price * x.Quantity);
			var deliveryMethod= await _unitOfWork.GetRepositry<DeliveryMethod>().GetAsync(DeliveryMethodId);
			var order= new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal);
			await _unitOfWork.GetRepositry<Order>().AddAsync(order);
			var rows= await _unitOfWork.CompleteAsync();
			if (rows <= 0)
				return null;
			return order;	

		}

		public Task<Order> GetOrderByIdForSpecficUser(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForSpecficUser(string buyerEmail)
		{
			throw new NotImplementedException();
		}

			this.unitOfWork = unitOfWork;
		}
}
