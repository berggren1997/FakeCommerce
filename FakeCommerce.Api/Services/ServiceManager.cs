using FakeCommerce.Api.Services.Auth;
using FakeCommerce.Api.Services.Basket;
using FakeCommerce.Api.Services.Products;
using FakeCommerce.DataAccess.Repositories.Interfaces;
using FakeCommerce.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FakeCommerce.Api.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IBasketService> _basketService;

        public ServiceManager(IRepositoryManager repositoryManager, UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>(() =>
                new ProductService(repositoryManager));

            _authService = new Lazy<IAuthService>(() =>
                new AuthService(userManager, configuration));

            _basketService = new Lazy<IBasketService>(() =>
                new BasketService(repositoryManager));
        }
        public IProductService ProductService => _productService.Value;
        public IAuthService AuthService => _authService.Value;
        public IBasketService BasketService => _basketService.Value;
    }
}
