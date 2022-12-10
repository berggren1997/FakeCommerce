using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FakeCommerce.Api.Filters
{
    public class ApiKeyAuth : Attribute, IAuthorizationFilter
    {
        private readonly string _expectedApiKey;
        public ApiKeyAuth()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _expectedApiKey = configuration["ApiSettings:Api-Key"];
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["api-key"];

            if(!string.Equals(apiKey, _expectedApiKey, StringComparison.Ordinal))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
