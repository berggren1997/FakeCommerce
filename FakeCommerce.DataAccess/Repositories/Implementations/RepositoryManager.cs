using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly CommerceDbContext _context;
        public RepositoryManager(CommerceDbContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository>(() =>
                new ProductRepository(context));
        }

        public IProductRepository ProductRepository => _productRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
