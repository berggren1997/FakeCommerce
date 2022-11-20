using FakeCommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Data
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
            :base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> ShoppingCart { get; set; }
        public DbSet<BasketItem> ShoppingCartItems { get; set; }
    }
}
