using FakeCommerce.Api.ViewModels.Basket;
using Stripe;
using Stripe.Checkout;

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
            if (string.IsNullOrEmpty(""))
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

        }

        public async Task CreatePaymentSession(List<BasketItemDto> items, string username)
        {
            var domain = "http://localhost:3000";

            var transformedItems = items.Select(item => new
            {
                description = item.Description,
                quantity = 1,
                price_data = new
                {
                    currency = "usd",
                    unit_amount = item.Price * 100,
                    product_data = new
                    {
                        name = item.Name,
                        images = new[] { item.PictureUrl }
                    },
                },
                price = item.Price,
            });

            var metaData = new Dictionary<string, string>
            {
                { username, "username"}
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = (List<SessionLineItemOptions>)transformedItems,
                Mode = "payment",
                //TODO: Skapa en successpage i React
                SuccessUrl = domain + "/success",
                CancelUrl = domain + "/checkout",
                Metadata = metaData
            };
        }
    }
}
