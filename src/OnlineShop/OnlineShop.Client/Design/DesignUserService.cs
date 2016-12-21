using OnlineShop.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.Client.Design
{
    class DesignUserService : IUserService
    {
        static List<User> users = new List<User>();

        static DesignUserService()
        {
            users.Add(new User()
            {
                Id = 0,
                Login = "TestCustomer1",
                Password = "Password",
                Name = "TestCustomerName1",
                Rank = Rank.Client
            });

            users.Add(new User()
            {
                Id = 1,
                Login = "TestCustomer2",
                Password = "Password",
                Name = "TestCustomerName2",
                Rank = Rank.Client
            });
        }

        public User Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            return users.Find(user => user.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
