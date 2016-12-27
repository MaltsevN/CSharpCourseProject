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
    class OrderItemRepository: IRepository<OrderItem>
    {
        private AppContext appContext;

        public OrderItemRepository()
        {
            appContext = new AppContext();
        }

        public OrderItemRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public OrderItem Create(OrderItem item)
        {
            return appContext.OrderItems.Add(item);
        }

        public void Delete(int id)
        {
            OrderItem orderItem = appContext.OrderItems.Find(id);
            if (orderItem != null)
                appContext.OrderItems.Remove(orderItem);
        }

        public OrderItem GetItem(int id)
        {
            return appContext.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetItemsList()
        {
            return appContext.OrderItems;
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public void Update(OrderItem item)
        {
            appContext.Entry<OrderItem>(item).State = EntityState.Modified;
        }
    }
}
