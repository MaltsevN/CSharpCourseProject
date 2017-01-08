
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
            Product pixel = new Product() {  Name = "Google Pixel", Price = new ProductPrice() { Price = 649.0, EffectiveDate = new DateTime(2016, 9, 24) } };
            Product galaxy = new Product() {  Name = "Samsung Galaxy S7", Price = new ProductPrice() { Price = 599.0, EffectiveDate = new DateTime(2016, 5, 5) } };
            Product onePlusTwo = new Product() {  Name = "OnePlus 2", Price = new ProductPrice() { Price = 274.78, EffectiveDate = new DateTime(2016, 2, 5) } };
            Product iPhone6_16GB = new Product() { Name = "Apple iPhone 6 16GB", Price = new ProductPrice() { Price = 380.0, EffectiveDate = new DateTime(2015, 7, 15) } };
            Product hTC_Desire = new Product() {  Name = "HTC Desire 626 16GB", Price = new ProductPrice() { Price = 268.0, EffectiveDate = new DateTime(2015, 8, 10) } };
            Product huawei = new Product() {  Name = "Huawei Honor 8", Price = new ProductPrice() { Price = 329.0, EffectiveDate = new DateTime(2015, 2, 10) } };

            context.Products.AddOrUpdate(p => p.Name, iPhone, pixel, galaxy, onePlusTwo, iPhone6_16GB, hTC_Desire, huawei);
            
            context.Users.AddOrUpdate(u => u.Login,
                    new User() { Login = "Administrator", Name = "Admin", Password = "81dc9bdb52d04dc20036dbd8313ed055", Rank = Rank.Admin });

            context.SaveChanges();
        }
    }
}
