using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IAuthenticationService
    {
        AuthenticationTokenDto AuthenticationToken { get; }
        Task SignIn(string login, string password);
    }
}
