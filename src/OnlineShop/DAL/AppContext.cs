using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DAL
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
