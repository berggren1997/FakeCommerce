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
            if (basket == null)
            {
                //TODO: Skapa en anonym shoppingcart
                return null;
            }
            // Check that product exists
            var product = await _repository.ProductRepository.GetProduct(productId, trackChanges: false);
            if (product == null)
            {
                //TODO: Kasta custom exception
                return null;
            }
            //Lägg till produkten i shopping-cart + spara
            basket.AddItem(product, quantity);
            await _repository.SaveAsync();
            //Returnera basketdto
            return MapBasketToDto(basket);
            //return new BasketDto
            //{
            //    Id = basket.Id,
            //    BuyerId = basket.BuyerId,
            //    BasketItems = basket.Items.Select(item => new BasketItemDto
            //    {
            //        ProductId = item.Product.Id,
            //        Name = item.Product.Name,
            //        Description = item.Product.Description,
            //        PictureUrl = item.Product.PictureUrl,
            //        Price = item.Product.Price,
            //        Quantity = item.Product.Price
            //    }).ToList()
            //};
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

        public Task<bool> RemoveItemFromBasket(int productId, int quantity)
        {
            throw new NotImplementedException();
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
                    Quantity = item.Product.Price,
                    ProductId = item.ProductId
                }).ToList()
            };
        }

    }
}
