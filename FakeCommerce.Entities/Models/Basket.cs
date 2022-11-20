namespace FakeCommerce.Entities.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
    }
}
