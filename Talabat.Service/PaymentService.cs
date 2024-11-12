using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.orderAgregrate;
using Talabat.Core.repositry.contract;
using Talabat.Core.service.contract;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
		{
			this._configuration = configuration;
			this._basketRepository = basketRepository;
			this._unitOfWork = unitOfWork;
		}
		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
			var basket = await _basketRepository.GetBasketAsync(basketId);
			if (basket is null)
				return null;
			if (basket.Items.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _unitOfWork.GetRepositry<Product>().GetAsync(item.Id);
					if (product.Price != item.Price)
						item.Price = product.Price;
				}
			}
			var SubTotal = basket.Items.Sum(x => x.Price * x.quantity);
			var deliveryMethod = await _unitOfWork.GetRepositry<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
			var deliveryCost = deliveryMethod?.Cost;
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var option = new PaymentIntentCreateOptions()
				{
					Amount = (long)SubTotal * 100 + (long)deliveryCost * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string>(){ "card" }
				};
				 paymentIntent=await service.CreateAsync(option);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
				return basket;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)SubTotal * 100 + (long)deliveryCost * 100,

				};
				paymentIntent = await service.UpdateAsync(basketId, options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret; 
			}
			return basket;
		}
	}
}
 