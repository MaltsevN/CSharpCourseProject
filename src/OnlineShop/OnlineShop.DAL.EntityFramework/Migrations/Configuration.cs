
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
            Product iPhone = new Product() { Id = 1, Name = "Apple iPhone 7 128GB", Price = new ProductPrice() { Price = 749.0, EffectiveDate = new DateTime(2016, 10, 15) } };
            Product pixel = new Product() { Id = 2, Name = "Google Pixel", Price = new ProductPrice() { Price = 649.0, EffectiveDate = new DateTime(2016, 9, 24) } };
            Product galaxy = new Product() { Id = 3, Name = "Samsung Galaxy S7", Price = new ProductPrice() { Price = 599.0, EffectiveDate = new DateTime(2016, 5, 5) } };

            context.Products.AddOrUpdate(p => p.Id, iPhone, pixel, galaxy);

            Order order1 = new Order()
            {
                Id = 1,
                Name = "Order #1",
                Status = Status.NotDecorated,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductId = iPhone.Id, Product = iPhone, Quantity = 2 },
                    new OrderItem() { ProductId = galaxy.Id, Product = galaxy, Quantity = 1 }
                }
            };

            Order order2 = new Order()
            {
                Id = 2,
                Name = "Order #2",
                Status = Status.Processing,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductId = pixel.Id, Product = pixel, Quantity = 1 }
                }
            };

            Order order3 = new Order()
            {
                Id = 3,
                Name = "Order #3",
                Status = Status.Confirmed,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductId = pixel.Id, Product = pixel, Quantity = 2 }
                }
            };

            Order order4 = new Order()
            {
                Id = 4,
                Name = "Order #4",
                Status = Status.Cancelled,
                PlacingDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductId = pixel.Id, Product = pixel, Quantity = 3 }
                }
            };

            if (context.Users.FirstOrDefault(u => u.Login == "TestUser") == null)
            {
                //User password = 12345
                context.Users.AddOrUpdate(u => u.Id,                                    
                    new User() { Id = 1, Login = "TestUser", Name = "User", Password = "827ccb0eea8a706c4c34a16891f84e7b", Rank = Rank.Client, Orders = new List<Order>() { order1, order2, order3, order4 } });
            }

            if (context.Users.FirstOrDefault(u => u.Login == "TestAdmin") == null)
            {
                //User password = 1234
                context.Users.AddOrUpdate(u => u.Id,
                    new User() { Id = 1, Login = "TestAdmin", Name = "Admin", Password = "81dc9bdb52d04dc20036dbd8313ed055", Rank = Rank.Admin });
            }
            context.SaveChanges();
        }
    }
}
