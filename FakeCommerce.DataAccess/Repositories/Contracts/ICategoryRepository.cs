using FakeCommerce.Entities.Models;

namespace FakeCommerce.DataAccess.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories(bool trackChanges);
    }
}
