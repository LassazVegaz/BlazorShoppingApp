using Microsoft.EntityFrameworkCore;
using PurchaseService;
using TrendingApp.Packages.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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


var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
