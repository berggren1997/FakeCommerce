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
            var basket = await _service.BasketService.GetBasket(GetBuyerId());
            if(basket == null)
            {
                basket = await CreateBasket();
            }
            var currentBasket = await _service.BasketService.AddItemToBasket(productId, quantity, basket.BuyerId);
            return Ok(currentBasket);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromBasket(int productId, int quantity)
        {
            var newBasket = await _service.BasketService.RemoveItemFromBasket(productId, quantity, GetBuyerId());
            return Ok(newBasket);
        }

        [HttpDelete("clearCartItem/{itemId}")]
        public async Task<IActionResult> ClearCartItem(int itemId)
        {
            var buyerId = GetBuyerId();
            var newBasket = await _service.BasketService.ClearCartItem(buyerId, itemId);

            return Ok(newBasket);
        }

        [HttpDelete("clearCart")]
        public async Task<IActionResult> ClearCart()
        {
            return Ok(await _service.BasketService.ClearCart(GetBuyerId()));
        }

        private async Task<BasketDto> CreateBasket()
        {
            var buyerId = User.Identity?.Name;

            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTime.Now.AddDays(30)
                };

                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            var basket = await _service.BasketService.CreateNewBasket(buyerId);
            return basket;
        }

        private string? GetBuyerId()
        {
            var userName = User!.Identity!.Name;
            var buyerId = Request.Cookies["buyerId"];
            return userName != null ? userName: buyerId;
        }
    }
}
