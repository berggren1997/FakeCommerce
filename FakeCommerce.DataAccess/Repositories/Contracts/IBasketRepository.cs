using FakeCommerce.Entities.Models;

namespace FakeCommerce.DataAccess.Repositories.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasket(string buyerId, bool trackChanges);

        void CreateBasket(Basket basket);
        void RemoveBasket(Basket basket);
    }
}
