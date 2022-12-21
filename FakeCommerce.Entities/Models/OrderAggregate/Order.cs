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
        public int SubTotal { get; set; }
        public int DeliveryFee { get; set; }

        //PaymentIntent - reason? Stripe
        public string PaymentIntentId { get; set; } = string.Empty;

        public int GetTotal() => SubTotal + DeliveryFee;

    }
}
