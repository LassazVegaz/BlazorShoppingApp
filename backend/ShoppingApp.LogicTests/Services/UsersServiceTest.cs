using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Logic.Services;

namespace ShoppingApp.LogicTest.Services;

internal class UsersServiceTest
{
    private ShoppingAppContext context = null!; // this will always be initialized in the Setup method

    [SetUp]
    public void Setup()
    {
        var ops = new DbContextOptionsBuilder<ShoppingAppContext>()
            .UseInMemoryDatabase("InTestMemDb")
            .Options;
        context = new ShoppingAppContext(ops);
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
    }

    [Test]
    // just a simple test. For now, tests are not the focus of this course
    public async Task CreateUserTest()
    {
        var usersService = new UsersService(context);
        var newUser = await usersService.CreateUser(new IdentityUser
        {
            Email = "abcd@abcd.com"
        });

        Assert.That(newUser.Email, Is.EqualTo("abcd@abcd.com"));
    }
}
