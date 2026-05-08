using LegacyShop.Models;

namespace LegacyShop.Services;

public class BonusPointService
{
    public int Calculate(CustomerType type, decimal afterDiscount)
    {
        int basePoints = (int)(afterDiscount / 100);

        return type switch
        {
            CustomerType.VIP => basePoints * 2,
            CustomerType.Regular => basePoints,
            _ => 0
        };
    }
}