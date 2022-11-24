using FakeCommerce.Api.ViewModels.Products;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.Api.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ProductService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ProductDto> GetProduct(int productId, bool trackChanges)
        {
            var product = await _repositoryManager.ProductRepository.GetProduct(productId, trackChanges);
            if (product == null) return null;
            
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category?.Name ?? "unknown",
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(bool trackChanges)
        {
            var products = await _repositoryManager.ProductRepository.GetProducts(trackChanges);
            
            if (products == null) return null;
            
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                Category = product.Category?.Name ?? "unknown",
            }).ToList();
        }
    }
}
