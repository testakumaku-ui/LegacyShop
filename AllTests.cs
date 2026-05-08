using Xunit;
using System.Collections.Generic;
using LegacyShop.Models;
using LegacyShop.Services;
using LegacyShop.Parsers;
using LegacyShop.Exceptions;

namespace LegacyShop.Tests
{
    public class AllTests
    {
        private readonly OrderItemParser parser = new();

        [Fact]
        public void Parse_Valid()
        {
            OrderItem item = parser.Parse("P1;Test;10;2;Books;1");

            Assert.Equal(20m, item.Total);
        }

        [Fact]
        public void Parse_Invalid()
        {
            Assert.Throws<InvalidOrderItemException>(() => parser.Parse("bad"));
        }

        [Fact]
        public void Parse_NegativePrice()
        {
            Assert.Throws<InvalidOrderItemException>(() =>
                parser.Parse("P1;Test;-10;1;Books;1"));
        }

        [Fact]
        public void Parse_UnknownCategory()
        {
            OrderItem item = parser.Parse("P1;Test;10;1;X;1");

            Assert.Equal(ProductCategory.Other, item.Category);
        }

 
        [Fact]
        public void VIP_Discount()
        {
            var service = new DiscountService();
            var order = new Order(CustomerType.VIP, "", "", new List<OrderItem>());

            decimal result = service.Calculate(order, 1000m);

            Assert.Equal(100m, result);
        }

        [Fact]
        public void Wholesale_Over50k()
        {
            var service = new DiscountService();
            var order = new Order(CustomerType.Wholesale, "", "", new List<OrderItem>());

            decimal result = service.Calculate(order, 60000m);

            Assert.Equal(9000m, result);
        }


        [Fact]
        public void Moscow_Free()
        {
            var service = new DeliveryService();
            var items = new List<OrderItem>
            {
                new("1", "", 1000, 10, ProductCategory.Books, 1)
            };

            var order = new Order(CustomerType.Regular, "Moscow", "", items);

            decimal result = service.Calculate(order, 10000m);

            Assert.Equal(0m, result);
        }

        [Fact]
        public void SPB()
        {
            var service = new DeliveryService();
            var order = new Order(CustomerType.Regular, "Saint Petersburg", "", new List<OrderItem>());

            decimal result = service.Calculate(order, 1000m);

            Assert.Equal(400m, result);
        }

        [Fact]
        public void VAT_Electronics()
        {
            var service = new VatService();
            var items = new List<OrderItem>
            {
                new("1", "", 100, 1, ProductCategory.Electronics, 1)
            };

            Assert.Equal(20m, service.Calculate(items));
        }

        [Fact]
        public void VAT_Food()
        {
            var service = new VatService();
            var items = new List<OrderItem>
            {
                new("1", "", 100, 1, ProductCategory.Food, 1)
            };

            Assert.Equal(0m, service.Calculate(items));
        }

        [Fact]
        public void Bonus_VIP()
        {
            var service = new BonusPointService();
            Assert.Equal(20, service.Calculate(CustomerType.VIP, 1000));
        }

        [Fact]
        public void Full_Calc()
        {
            var items = new List<OrderItem>
            {
                new("1", "Laptop", 60000, 1, ProductCategory.Electronics, 2)
            };

            var order = new Order(CustomerType.VIP, "Moscow", "", items);
            var service = new OrderCalculationService();

            OrderCalculationResult result = service.Calculate(order);

            Assert.True(result.Total > 0);
        }
    }
}