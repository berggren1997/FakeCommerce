using FakeCommerce.Api.Services;
using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Implementations;
using FakeCommerce.DataAccess.Repositories.Interfaces;
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

        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection service) =>
            service.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection service) =>
            service.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureJwt(this IServiceCollection service, IConfiguration configuration)
        {
            //TODO: Lägg till JWT-Auth
        }
    }
}
