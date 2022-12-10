using FakeCommerce.Api.Filters;
using FakeCommerce.Api.Services;
using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Implementations;
using FakeCommerce.DataAccess.Repositories.Interfaces;
using FakeCommerce.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                    policy.WithOrigins("http://localhost:3000");
                    policy.AllowCredentials();
                    //policy.AllowAnyOrigin();
                });
            });
        }

        public static void ConfigureIdentity(this IServiceCollection service)
        {
            var builder = service.AddIdentity<AppUser, AppRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 5;
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<CommerceDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection service) =>
            service.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection service) =>
            service.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureJwt(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureApiKeyAuth(this IServiceCollection service)
        {
            service.AddMvc(options =>
            {
                options.Filters.Add(new ApiKeyAuth());
            });
        }
    }
}
