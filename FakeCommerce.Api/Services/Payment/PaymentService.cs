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

        public Session CreateCheckoutSession(List<BasketItemDto> items, string username)
        {
            var secretKey = _configuration["StripeSettings:SecretKey"];
            StripeConfiguration.ApiKey = secretKey;

            var lineItems = new List<SessionLineItemOptions>();
            items.ForEach(item => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = item.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name,
                        Images = new List<string> { item.PictureUrl }
                    }
                },
                Quantity = item.Quantity,
            }));
            var metaData = new Dictionary<string, string>
            {
                { username, "user" }
            };
            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "US", "SE"}
                },
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                //Metadata = metaData,
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/checkout"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

        public async Task<bool> FulfillOrder(HttpRequest req)
        {
            var json = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    req.Headers["Stripe-Signature"],
                    "PLACEHOLDER FOR WEBHOOK-SECRET"
                );

                if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = "get the user";
                    //TODO: Place order the user, create orders repository etc
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
