using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public interface IAccountManager
    {
        AuthenticationTokenDto SignIn(string login, string password);
    }
}
