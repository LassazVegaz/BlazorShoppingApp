namespace PurchaseService.Models;

public class Item
{
    public int Id { get; set; }
    public double Price { get; set; }
    public List<User> Users { get; set; } = [];
}
