using FakeCommerce.Api.ViewModels.Basket;
using Stripe;
using Stripe.Checkout;

namespace FakeCommerce.Api.Services.Payment
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreateOrUpdatePaymentIntent(BasketDto basket);
        Session CreateCheckoutSession(List<BasketItemDto> items, string username);
    }
}
