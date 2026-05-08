namespace LegacyShop.Models;

public class OrderItem
{
    public string Sku { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Quantity { get; }
    public ProductCategory Category { get; }
    public decimal Weight { get; }

    public decimal Total => Price * Quantity;

    public OrderItem(string sku, string name, decimal price, int quantity,
        ProductCategory category, decimal weight)
    {
        Sku = sku;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
        Weight = weight;
    }
}