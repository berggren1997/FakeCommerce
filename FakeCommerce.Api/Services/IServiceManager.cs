using FakeCommerce.Api.Services.Products;

namespace FakeCommerce.Api.Services
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
    }
}
