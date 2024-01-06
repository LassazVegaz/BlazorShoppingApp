using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UsersService.Core.Data;
using UsersService.Core.Models;
using UsersService.Core.Options;
using LogicServices = UsersService.Logic.Services;

namespace UsersService.LogicTest.Services;

internal class UsersServiceTest
{
    private ServiceProvider serviceProvider = null!;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddOptions<UserOptions>();
        services.AddDbContext<ShoppingAppContext>(ops
            => ops.UseInMemoryDatabase("InTestMemDb"));

        serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        serviceProvider.Dispose();
    }

    [Test]
    // just a simple test. For now, tests are not the focus of this course
    public async Task EmailExistsTestAsync()
    {
        var context = serviceProvider.GetRequiredService<ShoppingAppContext>();
        var userOptions = serviceProvider.GetRequiredService<IOptions<UserOptions>>();

        context.Users.Add(new User { Email = "abcd@abcd.com" });
        await context.SaveChangesAsync();

        var usersService = new LogicServices.UsersService(userOptions, context);
        Assert.Multiple(async () =>
        {
            Assert.That(await usersService.EmailExists("abcd@abcd.com"));
            Assert.That(!await usersService.EmailExists("abcd2@abcd.com"));
        });
    }
}
