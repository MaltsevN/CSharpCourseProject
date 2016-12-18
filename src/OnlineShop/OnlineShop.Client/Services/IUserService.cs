using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IUserService
    {
        void Create(User user);
        void Delete(int id);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        void Update(User user);
    }
}
