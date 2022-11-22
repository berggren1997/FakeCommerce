using FakeCommerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.Api.Extensions.Service
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        }

        public static void ConfigureJwt(this IServiceCollection service, IConfiguration configuration)
        {

        }
    }
}
