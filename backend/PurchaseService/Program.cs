using Microsoft.EntityFrameworkCore;
using PurchaseService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// db context
builder.Services.AddDbContext<PurchaseServiceContext>(ops =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var version = builder.Configuration.GetValue<string>("MySqlVersion");
    ops.UseMySql(connectionString, new MySqlServerVersion(version));
});


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
