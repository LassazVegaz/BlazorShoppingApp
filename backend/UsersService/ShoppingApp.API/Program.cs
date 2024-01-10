using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.ApiExceptionsHandler;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.Cors;
using TrendingApp.Packages.MassTransitDependencyInjection;
using UsersService;
using UsersService.Constants;
using UsersService.Core;
using UsersService.Options;

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
builder.Services.AddDbContext<UsersServiceContext>(ops =>
    ops.UseMySql(builder.Configuration.GetConnectionString("ShoppingAppDb"), mySqlVersion));

// mapper
builder.Services.AddAutoMapper(typeof(Mapper));

// MassTransit
builder.Services.AddTrendingAppMassTransit();

// app services
builder.Services.AddScoped<IUsersService, UsersManager>();

var app = builder.Build();

app.UseExceptionHandler(ops => { });

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
