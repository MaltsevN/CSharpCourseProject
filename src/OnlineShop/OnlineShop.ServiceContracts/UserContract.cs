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

        [Authorize(new[] { RankDto.Admin })]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public UserDto Create(UserDto item)
        {
            return userManager.Create(item);
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            userManager.Delete(id);
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetUser/{id}")]
        public UserDto GetItem(string id)
        {
            return userManager.GetUser(Convert.ToInt32(id));
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllUsers")]
        public IEnumerable<UserDto> GetItemsList()
        {
            return userManager.GetAllUsers();
        }

        [Authorize(new[] { RankDto.Admin})]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(UserDto item)
        {
            userManager.Update(item);
        }
    }
}

