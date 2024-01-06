using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Core.Data;
using UsersService.Core.Options;

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
}
