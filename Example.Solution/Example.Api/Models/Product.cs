namespace Example.Api.Models;

public partial class Product
{
    public int IdProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; } = null!;
}
