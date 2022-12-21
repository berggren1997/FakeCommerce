using FakeCommerce.Entities.Models;
using FakeCommerce.Entities.Models.OrderAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Data
{
    public class CommerceDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
            :base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> ShoppingCart { get; set; }
        public DbSet<BasketItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
