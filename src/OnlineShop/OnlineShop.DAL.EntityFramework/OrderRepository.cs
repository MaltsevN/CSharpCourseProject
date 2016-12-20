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

        public void Create(Order item)
        {
            appContext.Orders.Add(item);
        }

        public void Delete(int id)
        {
            Order order = appContext.Orders.Find(id);
            if (order != null)
                appContext.Orders.Remove(order);
        }

        public Order GetItem(int id)
        {
            return appContext.Orders.Include(or => or.User).Include(or => or.Items.Select(i => i.Product.Price)).FirstOrDefault(or => or.Id == id);
        }

        public IEnumerable<Order> GetItemsList()
        {
            return appContext.Orders.Include(or => or.User).Include(or => or.Items.Select(i => i.Product.Price));
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public void Update(Order item)
        {
            appContext.Entry<Order>(item).State = EntityState.Modified;
        }
    }
}
