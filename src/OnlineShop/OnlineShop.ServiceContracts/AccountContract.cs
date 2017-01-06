using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DTO;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using OnlineShop.BL;

namespace OnlineShop.ServiceContracts
{
    public class AccountContract : IAccountContract
    {
        private readonly IAccountManager accountManager;

        public AccountContract(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public AuthenticationTokenDto SignIn(string login, string password)
        {
            return accountManager.SignIn(login, password);
        }
    }
}
