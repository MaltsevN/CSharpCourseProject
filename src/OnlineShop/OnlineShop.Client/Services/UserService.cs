using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace OnlineShop.Client.Services
{
    class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            client = new HttpClient();
        }

        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            User user = null;
            string requestUri = Properties.Resources.UrlToServer + "User/GetUser/" + id;
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(jsonResult);
            }

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
