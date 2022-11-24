using FakeCommerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FakeCommerce.Api.ContextFactory
{
    public class CommerceContextContextFactory : IDesignTimeDbContextFactory<CommerceDbContext>
    {
        public CommerceDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CommerceDbContext>()
                .UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly("FakeCommerce.Api"));

            return new CommerceDbContext(builder.Options);
        }
    }
}
