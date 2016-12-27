using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IUserService
    {
        UserDto Create(UserDto user);
        void Delete(int id);
        UserDto GetUser(int id);
        IEnumerable<UserDto> GetUsers();
        void Update(UserDto user);
    }
}
