using FakeCommerce.Entities.Models;

namespace FakeCommerce.DataAccess.Repositories.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasket(string buyerId, bool trackChanges);
    }
}
