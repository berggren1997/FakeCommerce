using FakeCommerce.Api.Extensions.Exceptions;
using FakeCommerce.Api.Extensions.Service;
using FakeCommerce.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlConnection(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCors();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureApiKeyAuth();
builder.Services.ConfiugreSwaggerAuthentication();
var app = builder.Build();

app.ConfigureExceptionHandler();

DbInitializer.SeedData(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
