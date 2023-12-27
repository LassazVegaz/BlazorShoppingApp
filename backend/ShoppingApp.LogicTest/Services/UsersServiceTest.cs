using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Models;
using ShoppingApp.Logic.Services;

namespace ShoppingApp.LogicTest.Services;

internal class UsersServiceTest
{
    private ShoppingAppContext context = null!; // this will always be initialized in the Setup method

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddDbContext<ShoppingAppContext>(ops =>
                   ops.UseInMemoryDatabase("InTestMemDb"));

        var serviceProvider = services.BuildServiceProvider();

        context = serviceProvider.GetRequiredService<ShoppingAppContext>();
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    // just a simple test. For now, tests are not the focus of this course
    public async Task EmailExistsTestAsync()
    {
        context.Users.Add(new User { Email = "abcd@abcd.com" });
        await context.SaveChangesAsync();

        var usersService = new UsersService(context);
        Assert.Multiple(async () =>
        {
            Assert.That(await usersService.EmailExists("abcd@abcd.com"));
            Assert.That(!await usersService.EmailExists("abcd2@abcd.com"));
        });
    }
}
