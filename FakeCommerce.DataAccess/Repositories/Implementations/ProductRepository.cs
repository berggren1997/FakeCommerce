using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(CommerceDbContext context) : base(context)
        { }

        public async Task<Product?> GetProduct(int productId, bool trackChanges)
        {
            var product = await FindByCondition(x => x.Id == productId, trackChanges)
                .Include(x => x.Category)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .Include(x => x.Category)
                .ToListAsync();
            
            return products;
        }
    }
}
