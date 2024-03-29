﻿using FakeCommerce.Api.ViewModels.Basket;

namespace FakeCommerce.Api.Services.Basket
{
    public interface IBasketService
    {
        Task<BasketDto> CreateNewBasket(string buyerId);
        Task<BasketDto> GetBasket(string buyerId);
        Task<BasketDto> AddItemToBasket(int productId, int quantity, string buyerId);
        Task<BasketDto> RemoveItemFromBasket(int productId, int quantity, string buyerId);
        Task<BasketDto> ClearCart(string buyerId);
        Task<BasketDto> ClearCartItem(string buyerId, int productId);
        Task<BasketDto> TransferAnonymousBasket(string buyerId, string username);
        Task DeleteBasket(string username);
    }
}
