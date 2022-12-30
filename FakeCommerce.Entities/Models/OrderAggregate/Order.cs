namespace FakeCommerce.Entities.Models.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; } = new();
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public OrderStatus OrderStatus { get; set; }
        public int Total { get; set; }

        public int GetTotal()
        {
            var total = 0;
            foreach (var item in OrderItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

    }
}
