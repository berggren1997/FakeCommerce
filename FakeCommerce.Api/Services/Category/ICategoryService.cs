using FakeCommerce.Api.ViewModels;

namespace FakeCommerce.Api.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllProductCategories(bool trackChanges);
    }
}
