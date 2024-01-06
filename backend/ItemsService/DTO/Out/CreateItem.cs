namespace ItemsService.DTO.Out;

public record CreateItem
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
}
