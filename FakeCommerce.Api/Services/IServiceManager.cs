using FakeCommerce.Api.Services.Auth;
using FakeCommerce.Api.Services.Basket;
using FakeCommerce.Api.Services.Products;

namespace FakeCommerce.Api.Services
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IAuthService AuthService { get; }
        IBasketService BasketService { get; }
    }
}
