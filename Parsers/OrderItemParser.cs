using System.Globalization;
using LegacyShop.Models;
using LegacyShop.Exceptions;

namespace LegacyShop.Parsers;

public class OrderItemParser
{
    public OrderItem Parse(string input)
    {
        var parts = input.Split(';');

        if (parts.Length != 6)
            throw new InvalidOrderItemException();

        decimal price = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
        int quantity = int.Parse(parts[3]);
        decimal weight = decimal.Parse(parts[5], CultureInfo.InvariantCulture);

        if (price <= 0 || quantity <= 0 || weight < 0)
            throw new InvalidOrderItemException();

        var category = parts[4] switch
        {
            "Electronics" => ProductCategory.Electronics,
            "Books" => ProductCategory.Books,
            "Food" => ProductCategory.Food,
            _ => ProductCategory.Other
        };

        return new OrderItem(parts[0], parts[1], price, quantity, category, weight);
    }
}
