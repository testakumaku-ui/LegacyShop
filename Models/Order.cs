using System.Collections.Generic;

namespace LegacyShop.Models;

public class Order
{
    public CustomerType CustomerType { get; }
    public string City { get; }
    public string Coupon { get; }
    public List<OrderItem> Items { get; }

    public Order(CustomerType customerType, string city, string coupon, List<OrderItem> items)
    {
        CustomerType = customerType;
        City = city;
        Coupon = coupon;
        Items = items;
    }
}