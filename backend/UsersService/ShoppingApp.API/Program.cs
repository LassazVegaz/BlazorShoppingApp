using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.ApiExceptionsHandler;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.Cors;
using TrendingApp.Packages.MassTransitDependencyInjection;
using UsersService.API.Constants;
using UsersService.API.Mapper;
using UsersService.Core.Data;
using UsersService.Core.Options;
using UsersService.Core.Services;
using Services = UsersService.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// options
builder.Services.Configure<UserOptions>(builder.Configuration.GetSection(OptionsNames.UserOptions));

// cors
builder.Services.AddTrendingAppCors();

// Exception handlers
builder.Services.AddTrendingAppExceptionHandlers();

// authentication
builder.Services.AddTrendingAppAuthentication();
builder.Services.AddTokensService();

builder.Services.AddAuthorization();

// db context
var mySqlVersion = new MySqlServerVersion(builder.Configuration.GetValue("MySQLVersion", "8.0.29"));
builder.Services.AddDbContext<ShoppingAppContext>(ops =>
    ops.UseMySql(builder.Configuration.GetConnectionString("ShoppingAppDb"), mySqlVersion));

// mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// MassTransit
builder.Services.AddTrendingAppMassTransit();

// app services
builder.Services.AddScoped<IUsersService, Services.UsersService>();
builder.Services.AddScoped<IAuthService, Services.AuthService>();

var app = builder.Build();

app.UseExceptionHandler(ops => { });

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
