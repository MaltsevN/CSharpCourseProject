
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace OnlineShop.DAL.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppContext context)
        {
            Product iPhone = new Product() { Name = "Apple iPhone 7 128GB", Price = new ProductPrice() { Price = 749.0, EffectiveDate = new DateTime(2016, 10, 15) } };
            Product pixel = new Product() { Name = "Google Pixel", Price = new ProductPrice() { Price = 649.0, EffectiveDate = new DateTime(2016, 9, 24) } };
            Product galaxy = new Product() { Name = "Samsung Galaxy S7", Price = new ProductPrice() { Price = 599.0, EffectiveDate = new DateTime(2016, 5, 5) } };

            context.Products.AddOrUpdate(p => p.Name, iPhone, pixel, galaxy);

            Order order1 = new Order()
            {
                Name = "Order #1",
                Status = Status.NotDecorated,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { Product = iPhone, Quantity = 2 },
                    new OrderItem() { Product = galaxy, Quantity = 1 }
                }
            };

            Order order2 = new Order()
            {
                Name = "Order #2",
                Status = Status.NotDecorated,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { Product = pixel, Quantity = 1 }
                }
            };

            context.Users.AddOrUpdate(u => u.Login,
                new User() { Login = "TestUser", Name = "User", Password = "testpassword", Rank = Rank.Client, Orders = new List<Order>() { order1, order2 } });

            context.SaveChanges();
        }
    }
}
