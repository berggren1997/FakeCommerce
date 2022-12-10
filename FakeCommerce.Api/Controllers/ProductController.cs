using FakeCommerce.Api.Filters;
using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _service.ProductService.GetProducts(trackChanges: false);

            return products != null ? Ok(products) : NotFound("No products found");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _service.ProductService.GetProduct(id, trackChanges: false);
            return product != null ? Ok(product) : NotFound("No product found");
        }
    }
}
