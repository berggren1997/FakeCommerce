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
            // Check that product exists
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            if (product == null)
            {
                //TODO: Kasta custom exception
                return null;
            }
            //basket kan inte vara null här, då kontrollern säkerställer att vi har/skapar en
            //ny shoppingcart innan det läggs till
            basket!.AddItem(product, quantity);
            await _repository.SaveAsync();
            
            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> GetBasket(string buyerId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: false);
            
            if (basket is null)
            {
                //TODO: Kasta eget fel
                return null;
            }

            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> RemoveItemFromBasket(int productId, int quantity, string buyerId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            
            if (basket == null) throw new Exception($"Temporary exception: Basket not found in: " +
                $"{nameof(RemoveItemFromBasket)} method");
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            
            if(product == null) throw new Exception($"Temporary exception: Product not found in: " +
                $"{nameof(RemoveItemFromBasket)} method, and can not be removed");

            basket.RemoveItem(productId, quantity);
            await _repository.SaveAsync();
            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> ClearCart(string buyerId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            if (basket == null) 
                throw new Exception($"No basket found in: {nameof(ClearCart)} method in BasketService");
            
            basket.Items.Clear();
            await _repository.SaveAsync();

            return MapBasketToDto(basket);
        }
        
        public async Task<BasketDto> CreateNewBasket(string buyerId)
        {
            var basket = new Entities.Models.Basket
            {
                BuyerId = buyerId
            };
            _repository.BasketRepository.CreateBasket(basket);
            await _repository.SaveAsync();

            return MapBasketToDto(basket);

        }

        /// <summary>
        /// doesnt access instance data, and can be marked as static
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        private static BasketDto MapBasketToDto(Entities.Models.Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                BasketItems = basket.Items.Select(item => new BasketItemDto
                {
                    Name = item.Product.Name,
                    Description = item.Product.Description,
                    PictureUrl = item.Product.PictureUrl,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                }).ToList()
            };
        }

    }
}
