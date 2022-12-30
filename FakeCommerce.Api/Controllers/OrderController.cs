using FakeCommerce.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrderController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUserOrders()
        {
            //var orders = await _service.OrderService.GetUserOrders(User?.Identity?.Name);
            
            return Ok(/*orders*/);
        }
    }
}
