using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.Api.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IRepositoryManager _repository;

        public BasketService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<BasketDto> AddItemToBasket(int productId, int quantity, string buyerId)
        {
            //Få fram användarens shopping-cart
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            //var basket2 = await RetrieveBasket(buyerId);
            if(basket == null)
            {
                //TODO: Kasta custom exception
                return null;
            }
            // Check that product exists
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            if(product == null)
            {
                //TODO: Kasta custom exception
                return null;
            }
            //Lägg till produkten i shopping-cart + spara
            basket.AddItem(product, quantity);
            await _repository.SaveAsync();
            //Returnera basketdto
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                BasketItems = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.Product.Id,
                    Name = item.Product.Name,
                    Description = item.Product.Description,
                    PictureUrl = item.Product.PictureUrl,
                    Price = item.Product.Price,
                    Quantity = item.Product.Price
                }).ToList()
            };
        }

        public Task<bool> RemoveItemFromBasket(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        // Sketchy, men kanske kan använda
        private async Task<FakeCommerce.Entities.Models.Basket?> RetrieveBasket(string buyerId)
        {
            return await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
        }
    }
}
