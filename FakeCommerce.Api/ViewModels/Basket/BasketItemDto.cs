namespace FakeCommerce.Api.ViewModels.Basket
{
    public class BasketItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
