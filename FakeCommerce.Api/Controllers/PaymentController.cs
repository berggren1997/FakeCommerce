using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.Api.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PaymentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("create-checkout-session"), Authorize]
        public async Task<IActionResult> Create([FromBody] List<BasketItemDto> items)
        {
            var username = User?.Identity?.Name;
            items.ForEach(item => Console.WriteLine(new { username, id = item.ProductId, name = item.Name}));
            return Ok();
        }
    }
}
