using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.EntityFramework
{
    public class AppContext : DbContext
    {
        static AppContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, Migrations.Configuration>());
        }

        public AppContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(user => user.Login).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Login") { IsUnique = true }));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
