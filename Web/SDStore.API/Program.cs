using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using SDStore.Data;
using SDStore.API.ActionFilters;
using SDStore.API.OrderCart;
using SDStore.API.RouteConstraints;
using SDStore.Services;
using SDStore.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

// TODO: Maybe extract to an extension method and decorate services with ServiceLifetime so we don't manually write them, but loop them with Reflection
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("email", typeof(EmailRouteConstraint));
});

builder.Services.AddHttpClient<ICartOperations, Cart>();
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// TODO: Maybe move this into extension methods to have less code here, just one liners?
builder.Services.AddDbContext<SDStoreDBContext>(options =>
{
    // TODO: Use SecretManager, Azure Key Vault, or some other key storage. For now this will do, just replace the connStr.
    options.UseSqlServer(@"Server=DEV;Database=SDStore;Trusted_Connection=True;TrustServerCertificate=True;");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Software Dynamics Store")
            .WithModels(false)
            .WithLayout(ScalarLayout.Classic);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();