using Microsoft.EntityFrameworkCore;
using ShoppingApp.API.Mapper;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Services;
using ShoppingApp.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context
var mySqlVersion = new MySqlServerVersion(builder.Configuration.GetValue("MySQLVersion", "8.0.29"));
builder.Services.AddDbContext<ShoppingAppContext>(ops =>
    ops.UseMySql(builder.Configuration.GetConnectionString("ShoppingAppDb"), mySqlVersion));

// mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// app services
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
