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
            string jsonRequest = serializer.Serialize(user);
            string requestUri = Properties.Resources.UrlToServer + "User/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                user = serializer.Deserialize<UserDto>(jsonResult);
            }
            else
            {
                user = null;
            }

            return user;
        }

        public void Delete(int id)
        {
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "User/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
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
            IEnumerable<UserDto> users = new UserDto[0];
            string requestUri = Properties.Resources.UrlToServer + "User/GetAllUsers";
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                users = serializer.Deserialize<IEnumerable<UserDto>>(jsonResult);
            }

            return users;
        }

        public void Update(UserDto user)
        {
            string jsonRequest = serializer.Serialize(user);
            string requestUri = Properties.Resources.UrlToServer + "User/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
