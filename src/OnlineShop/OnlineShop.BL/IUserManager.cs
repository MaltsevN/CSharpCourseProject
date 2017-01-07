using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public interface IUserManager
    {
        UserDto Create(UserDto item);
        void Delete(int id);
        UserDto GetUser(int id);
        IEnumerable<UserDto> GetAllUsers();
        void Update(UserDto item);
    }
}
