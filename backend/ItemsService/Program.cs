using ItemsService.Context;
using ItemsService.Core;
using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.Authentication.Extensions;
using Services = ItemsService.Services;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// authentication
builder.Services.AddTrendingAppAuthentication();

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

// app services
builder.Services.AddScoped<IItemsService, Services.ItemsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.MapControllers();

app.Run();
