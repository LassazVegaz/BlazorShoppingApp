using ItemsService;
using ItemsService.Core;
using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.MassTransitDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// authentication
builder.Services.AddTrendingAppAuthentication();

// authorization
builder.Services.AddAuthorization();

// automapper
builder.Services.AddAutoMapper(typeof(Mapper));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context
builder.Services.AddDbContext<ItemsServiceContext>(ops =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var version = builder.Configuration.GetValue<string>("MySqlVersion");
    ops.UseMySql(connectionString, new MySqlServerVersion(version));
});

// MassTransit
builder.Services.AddTrendingAppMassTransit();

// app services
builder.Services.AddScoped<IItemsManager, ItemsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
