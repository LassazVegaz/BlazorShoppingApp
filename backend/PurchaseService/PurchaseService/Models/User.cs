namespace PurchaseService.Models;

public class User
{
    public int Id { get; set; }
    public double Credits { get; set; }
    public List<Item> Items { get; set; } = [];
}
