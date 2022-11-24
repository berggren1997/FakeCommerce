namespace FakeCommerce.Api.ViewModels.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
    }
}
