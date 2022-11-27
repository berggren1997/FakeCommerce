using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(CommerceDbContext context) : base(context)
        { }
        public async Task<IEnumerable<Category>> GetCategories(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();
    }
}
