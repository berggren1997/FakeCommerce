namespace FakeCommerce.Entities.Models
{
    public class AppUser
    {
        public string Username { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
