using FakeCommerce.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var categories = await _service.CategoryService.GetAllProductCategories(trackChanges: false);
            return categories != null ? Ok(categories) : NotFound("No categories retrieved from database");
        }
    }
}
