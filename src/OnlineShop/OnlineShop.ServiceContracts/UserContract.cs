using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using OnlineShop.DTO;
using OnlineShop.BL;

namespace OnlineShop.ServiceContracts
{
    public class UserContract : IUserContract
    {
        private readonly IUserManager userManager;

        public UserContract(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public UserDto Create(UserDto item)
        {
            return userManager.Create(item);
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            userManager.Delete(id);
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetUser/{id}")]
        public UserDto GetItem(string id)
        {
            return userManager.GetUser(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllUsers")]
        public IEnumerable<UserDto> GetItemsList()
        {
            return userManager.GetAllUsers();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(UserDto item)
        {
            
        }
    }
}

