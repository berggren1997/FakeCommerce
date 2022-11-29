using FakeCommerce.Api.ViewModels;
using FakeCommerce.DataAccess.Repositories.Interfaces;

namespace FakeCommerce.Api.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repository;

        public CategoryService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllProductCategories(bool trackChanges)
        {
            var categories = await _repository.CategoryRepository.GetCategories(trackChanges);
            
            if (categories == null)
                return null;

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Category = c.Name
            }).ToList();
        }
    }
}
