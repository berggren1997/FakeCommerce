using FakeCommerce.Api.ViewModels.Basket;
using Stripe;
using Stripe.Checkout;

namespace FakeCommerce.Api.Services.Payment
{
    public interface IPaymentService
    {
        Session CreateCheckoutSession(List<BasketItemDto> items, string username);
        
        /// <summary>
        /// Webhook handler that is called when a payment session is completed
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> FulfillOrder(HttpRequest req, string buyerId);
    }
}
