using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.Entities.Models.OrderAggregate
{
    [Owned]
    public class ProductItemOrdered
    {
        public int ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
    }
}
