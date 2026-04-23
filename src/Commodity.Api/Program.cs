using Microsoft.EntityFrameworkCore;
using Commodity.Api.Services;
using Commodity.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CommodityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(3),
            errorNumbersToAdd: null)));

builder.Services.AddScoped<ICommodityService, CommodityService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommodityDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok($"The Commodity {builder.Configuration["Environment"]} API is up and running."));

app.MapGet("/health", () => Results.Ok());

app.MapGet("/info", () => Results.Ok("The Commodity API allows commodity data to be managed!"));

app.MapControllers();

app.Run();
