using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainModel;
using DAL;

namespace OnlineShop.DAL.EntityFramework
{
    public class OrderRepository : IRepository<Order>
    {
        private AppContext appContext;

        public OrderRepository()
        {
            appContext = new AppContext();
        }

        public OrderRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public Order Create(Order item)
        {
            item.User = appContext.Users.Find(item.UserId);
            return appContext.Orders.Add(item);
        }

        public void Delete(int id)
        {
            Order order = appContext.Orders.Find(id);
            if (order != null)
                appContext.Orders.Remove(order);
        }

        public Order GetItem(int id)
        {
            return appContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetItemsList()
        {
            return appContext.Orders;
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public void Update(Order item)
        {
            item.User = appContext.Users.Find(item.UserId);
            appContext.Entry<Order>(item).State = EntityState.Modified;
        }
    }
}
