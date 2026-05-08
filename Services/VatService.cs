using System.Collections.Generic;
using LegacyShop.Models;

namespace LegacyShop.Services;

public class VatService
{
    public decimal Calculate(List<OrderItem> items)
    {
        decimal vat = 0;

        foreach (var item in items)
        {
            vat += item.Category switch
            {
                ProductCategory.Electronics => item.Total * 0.20m,
                ProductCategory.Books => item.Total * 0.10m,
                ProductCategory.Food => 0,
                _ => item.Total * 0.20m
            };
        }

        return vat;
    }
}