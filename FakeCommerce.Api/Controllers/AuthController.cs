using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

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
            
            SetRefreshToken(jwtKit.RefreshToken);
            
            return Ok(new { accessToken = jwtKit.AccessToken, username = jwtKit.Username });
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
    }
}
