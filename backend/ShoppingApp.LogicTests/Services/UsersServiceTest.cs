using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Core.Data;
using ShoppingApp.Logic.Configure;

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
        services.ConfigureIdentityOptions();

        var serviceProvider = services.BuildServiceProvider();

        context = serviceProvider.GetRequiredService<ShoppingAppContext>();
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
    }

    [Test]
    // just a simple test. For now, tests are not the focus of this course
    public void EmailExistsTest()
    {
        Assert.Fail("Not implemented");
    }
}
