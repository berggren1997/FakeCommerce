using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.DataAccess.Repositories.Interfaces;
using FakeCommerce.Entities.Models.OrderAggregate;
using Stripe;
using Stripe.Checkout;

namespace FakeCommerce.Api.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _configuration;

        private const string _whSecret = "whsec_af2ab480d565c609162b606bcd161abad9feed5bfd3d8c9024acaced0370027f";
        public PaymentService(IConfiguration configuration, IRepositoryManager repository)
        {
            _configuration = configuration;
            _repository = repository;
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
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

        //TODO: inget här är testat, vet inte ens om det fungerar än
        public async Task<bool> FulfillOrder(HttpRequest req, string buyerId)
        {
            var json = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    req.Headers["Stripe-Signature"],
                    _whSecret
                );

                if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
                    if (basket == null) return false;
                    //TODO: Place order for the user
                    var newOrder = new Order
                    {
                        BuyerId = basket.BuyerId,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.PaymentReceived,
                        OrderItems = basket.Items.Select(x => new OrderItem
                        {
                            Id = x.Id,
                            Price = x.Product.Price,
                            ProductItemOrdered = new ProductItemOrdered
                            {
                                ProductName = x.Product.Name,
                                PictureUrl = x.Product.PictureUrl,
                                ProductItemId = x.Product.Id
                            }
                        }).ToList(),
                        Total = basket.Items.Sum(x => x.Product.Price * x.Quantity),
                    };
                    _repository.OrderRepository.CreateOrder(newOrder);
                    _repository.BasketRepository.RemoveBasket(basket);
                    await _repository.SaveAsync();
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
