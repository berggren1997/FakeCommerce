using Microsoft.AspNetCore.Identity;

namespace FakeCommerce.Entities.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
