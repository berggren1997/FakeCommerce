using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.DataAccess.Repositories.Interfaces;
using FakeCommerce.Entities.Exceptions.NotFoundExceptions;

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
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            
            if (product == null)
                throw new ProductNotFoundException(productId);
            
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
                throw new BasketNotFoundException();

            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> RemoveItemFromBasket(int productId, int quantity, string buyerId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            
            if (basket == null) throw new BasketNotFoundException();
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            
            if(product == null) throw new ProductNotFoundException(productId);

            basket.RemoveItem(productId, quantity);
            await _repository.SaveAsync();
            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> ClearCart(string buyerId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            if (basket == null) 
                throw new BasketNotFoundException();
            
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

        public async Task<BasketDto> ClearCartItem(string buyerId, int productId)
        {
            var basket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            if (basket == null) throw new BasketNotFoundException();
            //basket.Items = basket.Items.Where(x => x.Id != productId).ToList();
            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem != null)
            {
                basket.Items.Remove(basketItem);
                await _repository.SaveAsync();
            }

            return MapBasketToDto(basket);
        }

        public async Task<BasketDto> TransferAnonymousBasket(string buyerId, string username)
        {
            var anonymousBasket = await _repository.BasketRepository.GetBasket(buyerId, trackChanges: true);
            
            if (anonymousBasket == null)
                throw new BasketNotFoundException();

            var oldUserBasket = await _repository.BasketRepository.GetBasket(username, trackChanges: true);

            if(oldUserBasket != null)
            {
                _repository.BasketRepository.RemoveBasket(oldUserBasket);
            }

            anonymousBasket.BuyerId = username;
            await _repository.SaveAsync();
            return MapBasketToDto(anonymousBasket);
        }

        public async Task DeleteBasket(string username)
        {
            var basket = await _repository.BasketRepository.GetBasket(username, trackChanges: true);

            if(basket != null)
            {
                _repository.BasketRepository.RemoveBasket(basket);
                await _repository.SaveAsync();
            }
        }
    }
}
