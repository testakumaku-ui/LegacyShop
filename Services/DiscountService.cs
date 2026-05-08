using LegacyShop.Models;

namespace LegacyShop.Services;

public class DiscountService
{
    public decimal Calculate(Order order, decimal subtotal)
    {
        decimal discount = 0;

        if (order.CustomerType == CustomerType.VIP)
            discount = subtotal * 0.10m;

        else if (order.CustomerType == CustomerType.Wholesale && subtotal > 50000)
            discount = subtotal * 0.15m;

        if (order.Coupon == "SALE3" && order.CustomerType == CustomerType.Regular)
            discount += subtotal * 0.03m;

        if (order.Coupon == "FIX1000")
            discount += 1000;

        return discount > subtotal ? subtotal : discount;
    }
}