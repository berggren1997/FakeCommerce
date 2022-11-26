
using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ShoppingCartController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _service.BasketService.GetBasket(GetBuyerId());

            return basket != null ? Ok(basket) : NotFound("No basket found");
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(int productId, int quantity)
        {
            var currentBasket = await _service.BasketService.GetBasket(GetBuyerId());
            if(currentBasket == null)
            {
                currentBasket = await CreateBasket();
            }
            var basket = await _service.BasketService.AddItemToBasket(productId, quantity, GetBuyerId());
            return Ok(basket);
        }

        private async Task<BasketDto> CreateBasket()
        {
            var buyerId = User.Identity.Name;

            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(30)
                };

                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            var basket = await _service.BasketService.CreateNewBasket(buyerId);
            return basket;
        }

        private string GetBuyerId() => User?.Identity?.Name ?? Request.Cookies["buyerId"];
    }
}
