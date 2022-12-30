using FakeCommerce.DataAccess.Repositories.Contracts;

namespace FakeCommerce.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        IBasketRepository BasketRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task SaveAsync();
    }
}
