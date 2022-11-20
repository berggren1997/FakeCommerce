namespace FakeCommerce.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>
        public Category? Category { get; set; }
    }
}
