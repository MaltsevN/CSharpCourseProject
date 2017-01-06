using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.ServiceContracts
{
    static class Extensions
    {
        public static AuthenticationTokenDto GetToken(this HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"];
            if (authorizationHeader != null)
            {
                var authorizationHeaders = authorizationHeader.Split(' ');

                if (authorizationHeaders[0].ToLower() == "bearer")
                {
                    string[] content = authorizationHeaders[1].Split(':');
                    string token = content[1];
                    string login = content[0];

                    return new AuthenticationTokenDto() { Login = login, Token = token };
                }
            }

            return null;
        }
    }
}
