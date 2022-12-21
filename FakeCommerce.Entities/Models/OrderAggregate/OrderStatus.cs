namespace FakeCommerce.Entities.Models.OrderAggregate
{
    public enum OrderStatus
    {
        Pending = 1,
        PaymentReceived,
        PaymentFailed
    }
}
