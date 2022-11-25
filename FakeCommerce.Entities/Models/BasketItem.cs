namespace FakeCommerce.Entities.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>
        public int BasketId { get; set; }
        public Basket Basket { get; set; } = new();
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();
    }
}
