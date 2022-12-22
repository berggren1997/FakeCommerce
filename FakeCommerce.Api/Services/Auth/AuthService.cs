using FakeCommerce.Api.ViewModels.User;
using FakeCommerce.Entities.Exceptions.NotFoundExceptions;
using FakeCommerce.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FakeCommerce.Api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        private AppUser? _currentUser;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateUser(AuthenticateUserDto authenticateUserDto)
        {
            _currentUser = await _userManager.FindByNameAsync(authenticateUserDto.Username);

            return _currentUser != null && 
                await _userManager.CheckPasswordAsync(_currentUser, authenticateUserDto.Password);
        }

        public async Task<JwtTokenDto> CreateJwtToken(bool populateRefreshToken)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = GenerateRefreshToken();

            _currentUser!.RefreshToken = refreshToken;

            if (populateRefreshToken)
                _currentUser.RefreshTokenExpireDate = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_currentUser!);

            return new JwtTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Username = _currentUser.UserName
            };
        }

        public async Task<JwtTokenDto> RefreshAccessToken(string refreshToken)
        {
            _currentUser = await _userManager.Users.Where(x => x.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();

            if (_currentUser == null || _currentUser.RefreshToken != refreshToken ||
                _currentUser.RefreshTokenExpireDate <= DateTime.Now)
            {
                //TODO: Kasta ett custom fel här
                throw new Exception($"Invalid token passed in {nameof(RefreshAccessToken)} method.");
            }

            return await CreateJwtToken(populateRefreshToken: false);
        }

        public async Task<IdentityResult> RegisterUser(CreateUserDto newUser)
        {
            var newUserEntity = new AppUser
            {
                UserName = newUser.Username,
                Email = newUser.Email
            };

            // TODO: Lägg till så att vi kastar ett eget fel ifall användare finns
            var result = await _userManager.CreateAsync(newUserEntity, newUser.Password);

            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _currentUser!.UserName),
                new Claim("userId", _currentUser.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(_currentUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"] ?? null,
                audience: _configuration["JwtSettings:Audience"] ?? null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }

        public async Task<JwtTokenDto> GetCurrentUser(string username)
        {
            _currentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (_currentUser == null)
            {
                throw new UserNotFoundException();
                //return new JwtTokenDto
                //{
                //    Username = "nananannanananannananana....... BATMAN",
                //    AccessToken = "nananannanananannananana....... BATMAN",
                //    RefreshToken = "nananannanananannananana....... BATMAN"
                //};
            }
            return await CreateJwtToken(false);
        }
    }
}
