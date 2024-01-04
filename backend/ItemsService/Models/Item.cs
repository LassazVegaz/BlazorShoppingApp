namespace ItemsService.Models;

public class Item
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
}
