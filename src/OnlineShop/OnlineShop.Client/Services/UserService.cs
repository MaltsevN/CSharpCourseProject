using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using OnlineShop.DTO;

namespace OnlineShop.Client.Services
{
    class UserService : IUserService
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public UserService()
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
        }

        public UserDto Create(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser(int id)
        {
            UserDto user = null;
            string requestUri = Properties.Resources.UrlToServer + "User/GetUser/" + id;
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                user = serializer.Deserialize<UserDto>(jsonResult);
            }
            return user;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void Update(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
