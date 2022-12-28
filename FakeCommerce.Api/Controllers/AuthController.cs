using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.Api.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakeCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto authenticateUserDto)
        {
            var userAuthenticated = await _service.AuthService.AuthenticateUser(authenticateUserDto);
            if (!userAuthenticated) 
                return BadRequest("Bad credentials");
            
            var jwtKit = await _service.AuthService.CreateJwtToken(populateRefreshToken: true);

            //var activeBasket = await GetActiveBasket(authenticateUserDto.Username);

            await TransferActiveBasket(jwtKit.Username);

            SetRefreshToken(jwtKit.RefreshToken);
            
            return Ok(new 
            { 
                accessToken = jwtKit.AccessToken, 
                username = jwtKit.Username, 
                //basket = activeBasket != null ? activeBasket : null
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto newUser)
        {
            var registerResult = await _service.AuthService.RegisterUser(newUser);

            return registerResult.Succeeded ? StatusCode(201) : BadRequest("Bad request from client");
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken == null)
            {
                return Unauthorized("No valid token");
            }

            var authResponse = await _service.AuthService.RefreshAccessToken(refreshToken);

            SetRefreshToken(authResponse.RefreshToken);

            return Ok(new { accessToken = authResponse.AccessToken, username = authResponse.Username });
        }

        [HttpGet("currentuser"), Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _service.AuthService.GetCurrentUser(User.Identity!.Name!);
            SetRefreshToken(user.RefreshToken);
            return Ok(new {accessToken = user.AccessToken, username = user.Username});
        }

        private void SetRefreshToken(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private async Task<BasketDto?> GetActiveBasket(string username)
        {
            var anonymousBasket = await _service.BasketService.GetBasket(GetCurrentBuyerId());
            var userBasket = await _service.BasketService.GetBasket(username);

            if(anonymousBasket != null)
            {
                if(userBasket != null)
                {
                    await _service.BasketService.DeleteBasket(username);
                }
                var newBasket = await _service.BasketService.TransferAnonymousBasket(GetCurrentBuyerId(), username);
                Response.Cookies.Delete("buyerId", new CookieOptions()
                {
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
                return newBasket;
            }

            return anonymousBasket != null ? anonymousBasket : userBasket;
        }

        private async Task TransferActiveBasket(string username)
        {
            var anonymousBuyerId = Request.Cookies["buyerId"];
            if(anonymousBuyerId != null)
            {
                await _service.BasketService.TransferAnonymousBasket(anonymousBuyerId, username);
                Response.Cookies.Delete("buyerId", new CookieOptions()
                {
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
            }
        }

        private string GetCurrentBuyerId()
        {
            var buyerId = Request.Cookies["buyerId"] ?? User?.Identity?.Name;

            return buyerId;
        }
    }
}
