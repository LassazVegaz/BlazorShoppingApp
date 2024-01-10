using Microsoft.EntityFrameworkCore;
using PurchaseService;
using PurchaseService.Core;
using PurchaseService.Managers;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.MassTransitDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// mamanger
builder.Services.AddScoped<IPurchaseManager, PurchaseManager>();

// db context
builder.Services.AddDbContext<PurchaseServiceContext>(ops =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var version = builder.Configuration.GetValue<string>("MySqlVersion");
    ops.UseMySql(connectionString, new MySqlServerVersion(version));
});

// auth
builder.Services.AddTrendingAppAuthentication();
builder.Services.AddAuthorization();

// masstransit
builder.Services.AddTrendingAppMassTransit();

// mapper
builder.Services.AddAutoMapper(typeof(Mapper));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
