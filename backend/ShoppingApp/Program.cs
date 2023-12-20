using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Services;
using ShoppingApp.Logic.Services;
using ShoppingApp.Web.Components;
using ShoppingApp.Web.Mapper;

var mySQLVersion = new MySqlServerVersion(new Version(8, 0, 29));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// ef core
builder.Services.AddDbContextFactory<ShoppingAppContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), mySQLVersion));

// automapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// custom services
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();