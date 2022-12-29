using FakeCommerce.Api.ViewModels.Basket;
using Stripe;

namespace FakeCommerce.Api.Services.Payment
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreateOrUpdatePaymentIntent(BasketDto basket);
        Task CreatePaymentSession(List<BasketItemDto> items, string username);
    }
}
