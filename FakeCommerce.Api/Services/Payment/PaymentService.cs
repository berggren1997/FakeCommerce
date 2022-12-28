using FakeCommerce.Api.ViewModels.Basket;
using Stripe;

namespace FakeCommerce.Api.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(BasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();

            var intent = new PaymentIntent();

            var subTotal = basket.BasketItems.Sum(item => item.Quantity * item.Price);

            //TODO: Lägg till ett paymentintent som prop för Basket entitet, och BasketDto
            if(string.IsNullOrEmpty(""))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = subTotal,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = subTotal,
                };
                //TODO: Lägg till basketdto's paymentintent som första argument
                await service.UpdateAsync("", options);
            }
            return intent;
            
            throw new NotImplementedException();
        }
    }
}
