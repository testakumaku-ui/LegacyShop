using System.Linq;
using LegacyShop.Models;

namespace LegacyShop.Services;

public class DeliveryService
{
    public decimal Calculate(Order order, decimal afterDiscount)
    {
        decimal weight = order.Items.Sum(i => i.Weight * i.Quantity);

        decimal delivery = order.City switch
        {
            "Moscow" => afterDiscount > 5000 ? 0 : 300,
            "Saint Petersburg" => 400,
            _ => 700 + weight * 80
        };

        if (order.Items.Any(i => i.Category == ProductCategory.Electronics))
            delivery += 200;

        return delivery;
    }
}