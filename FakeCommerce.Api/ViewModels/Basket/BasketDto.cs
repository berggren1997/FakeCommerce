namespace FakeCommerce.Api.ViewModels.Basket
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } = string.Empty;
        public List<BasketItemDto> BasketItems { get; set; } = new();
    }
}
