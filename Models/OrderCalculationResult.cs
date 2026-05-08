namespace LegacyShop.Models;

public class OrderCalculationResult
{
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Delivery { get; set; }
    public decimal Vat { get; set; }
    public int Points { get; set; }

    public decimal Total => Subtotal - Discount + Delivery + Vat;
}