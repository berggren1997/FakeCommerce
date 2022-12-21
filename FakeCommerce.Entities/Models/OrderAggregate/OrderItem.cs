namespace FakeCommerce.Entities.Models.OrderAggregate
{
    public class OrderItem
    {
        public int Id { get; set; }
        public ProductItemOrdered? ProductItemOrdered { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
