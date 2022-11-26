using FakeCommerce.Api.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace FakeCommerce.Api.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> AuthenticateUser(AuthenticateUserDto authenticateUserDto);
        Task<IdentityResult> RegisterUser(CreateUserDto newUser);
        Task<JwtTokenDto> CreateJwtToken(bool populateRefreshToken);
        Task<JwtTokenDto> RefreshAccessToken(string refreshToken);
    }
}
