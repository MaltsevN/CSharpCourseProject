using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    public class AuthenticationTokenDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
    }
}
