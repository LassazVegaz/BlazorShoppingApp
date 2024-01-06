using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PurchaseService;
using PurchaseService.Models;

namespace PurchaseServiceTest;

public class PurchaseManagerTest
{
    private ServiceProvider _provider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<PurchaseServiceContext>(options => options.UseInMemoryDatabase(databaseName: "TestDb"));

        _provider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }

    [Test]
    public void PruchaseTest()
    {
        var context = _provider.GetRequiredService<PurchaseServiceContext>();
        context.Users.Add(new User { Id = 1, Credits = 1000, });
        context.Items.Add(new Item { Id = 1, Price = 100, });
        context.SaveChanges();

        var purchaseManager = new PurchaseManager(context);

        Assert.DoesNotThrowAsync(async () => await purchaseManager.Purchase(1, 1));

        var user = context.Users.Find(1);
        var item = context.Items.Find(1);

        Assert.Multiple(() =>
        {
            Assert.That(user?.Credits, Is.EqualTo(900));

            Assert.That(item?.Users.Count, Is.EqualTo(1));
            Assert.That(user?.Items.Count, Is.EqualTo(1));

            Assert.That(item?.Users[0].Id, Is.EqualTo(1));
            Assert.That(user?.Items[0].Id, Is.EqualTo(1));
        });
    }

    [Test]
    public void InsufficientCreditsPruchaseTest()
    {
        var context = _provider.GetRequiredService<PurchaseServiceContext>();
        context.Users.Add(new User { Id = 1, Credits = 100, });
        context.Items.Add(new Item { Id = 1, Price = 1000, });
        context.SaveChanges();

        var purchaseManager = new PurchaseManager(context);

        Assert.ThrowsAsync<Exception>(async () => await purchaseManager.Purchase(1, 1));

        var user = context.Users.Find(1);
        var item = context.Items.Find(1);

        Assert.Multiple(() =>
        {
            Assert.That(user?.Credits, Is.EqualTo(100));

            Assert.That(item?.Users.Count, Is.EqualTo(0));
            Assert.That(user?.Items.Count, Is.EqualTo(0));
        });
    }
}