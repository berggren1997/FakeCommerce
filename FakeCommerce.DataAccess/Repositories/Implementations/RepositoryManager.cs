using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IBasketRepository> _basketRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly CommerceDbContext _context;

        public RepositoryManager(CommerceDbContext context)
        {
            _context = context;

            _productRepository = new Lazy<IProductRepository>(() =>
                new ProductRepository(context));

            _basketRepository = new Lazy<IBasketRepository>(() =>
                new BasketRepository(context));

            _categoryRepository = new Lazy<ICategoryRepository>(() =>
                new CategoryRepository(context));

            _orderRepository = new Lazy<IOrderRepository>(() =>
                new OrderRepository(context));
        }

        public IProductRepository ProductRepository => _productRepository.Value;
        public IBasketRepository BasketRepository => _basketRepository.Value;
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;
        public IOrderRepository OrderRepository => _orderRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
