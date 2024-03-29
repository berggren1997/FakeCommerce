﻿using FakeCommerce.Api.Services.Auth;
using FakeCommerce.Api.Services.Basket;
using FakeCommerce.Api.Services.Category;
using FakeCommerce.Api.Services.Payment;
using FakeCommerce.Api.Services.Products;

namespace FakeCommerce.Api.Services
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IAuthService AuthService { get; }
        IBasketService BasketService { get; }
        ICategoryService CategoryService { get; }
        IPaymentService PaymentService { get; }
    }
}
