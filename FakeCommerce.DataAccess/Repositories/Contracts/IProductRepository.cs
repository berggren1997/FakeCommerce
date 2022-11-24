using FakeCommerce.Entities.Models;

namespace FakeCommerce.DataAccess.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(bool trackChanges);
        Task<Product?> GetProduct(int productId, bool trackChanges);
    }
}
