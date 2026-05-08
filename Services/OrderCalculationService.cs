using System.Linq;
using LegacyShop.Models;
using LegacyShop.Exceptions;

namespace LegacyShop.Services;

public class OrderCalculationService
{
    private readonly DiscountService _discount = new();
    private readonly VatService _vat = new();
    private readonly DeliveryService _delivery = new();
    private readonly BonusPointService _bonus = new();

    public OrderCalculationResult Calculate(Order order)
    {
        if (order.Items == null || !order.Items.Any())
            throw new EmptyOrderException();

        decimal subtotal = order.Items.Sum(i => i.Total);

        decimal discount = _discount.Calculate(order, subtotal);
        decimal afterDiscount = subtotal - discount;

        return new OrderCalculationResult
        {
            Subtotal = subtotal,
            Discount = discount,
            Vat = _vat.Calculate(order.Items),
            Delivery = _delivery.Calculate(order, afterDiscount),
            Points = _bonus.Calculate(order.CustomerType, afterDiscount)
        };
    }
}