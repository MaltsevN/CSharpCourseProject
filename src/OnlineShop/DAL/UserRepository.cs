using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL
{
    public class UserRepository : IRepository<User>
    {
        private AppContext appContext;

        public UserRepository()
        {
            appContext = new AppContext();
        }

        public void Create(User item)
        {
            appContext.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = appContext.Users.Find(id);
            if (user != null)
                appContext.Users.Remove(user);
        }

        public User GetItem(int id)
        {
            return appContext.Users.Find(id);
        }

        public IEnumerable<User> GetItemsList()
        {
            return appContext.Users;
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
