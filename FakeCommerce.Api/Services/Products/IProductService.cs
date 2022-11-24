using FakeCommerce.Api.ViewModels.Products;

namespace FakeCommerce.Api.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(bool trackChanges);
        Task<ProductDto> GetProduct(int productId, bool trackChanges);
    }
}
