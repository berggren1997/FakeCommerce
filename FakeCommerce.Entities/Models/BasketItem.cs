namespace FakeCommerce.Entities.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Basket Basket { get; set; } = new();
        public Product Product { get; set; } = new();
    }
}
