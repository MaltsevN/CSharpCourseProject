using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DAL;

namespace OnlineShop.DAL.EntityFramework
{
    public class UserRepository : IRepository<User>
    {
        private AppContext appContext;

        public UserRepository()
        {
            appContext = new AppContext();
        }

        public UserRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public User Create(User item)
        {
            return appContext.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = appContext.Users.Find(id);
            if (user != null)
                appContext.Users.Remove(user);
        }

        public User GetItem(int id)
        {
            return appContext.Users.Include(us => us.Orders.Select(or => or.Items.Select(p => p.Product.Price))).FirstOrDefault(us => us.Id == id);
        }

        public IEnumerable<User> GetItemsList()
        {
            return appContext.Users.Include(us => us.Orders.Select(or => or.Items.Select(p => p.Product.Price)));
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public void Update(User item)
        {
            appContext.Entry<User>(item).State = EntityState.Modified;
        }
    }
}
