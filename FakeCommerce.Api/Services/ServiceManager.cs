using FakeCommerce.Api.Services.Products;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.Api.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _productService = new Lazy<IProductService>(() =>
                new ProductService(repositoryManager));
        }
        public IProductService ProductService => _productService.Value;
    }
}
