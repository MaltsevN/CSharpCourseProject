using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using OnlineShop.DTO;
using System.Net.Http.Headers;

namespace OnlineShop.Client.Services
{
    class UserService : IUserService
    {
        private readonly IAuthenticationService authService;
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public UserService(IAuthenticationService authService)
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
            this.authService = authService;
        }

        public async Task<UserDto> CreateAsync(UserDto user)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
            string jsonRequest = serializer.Serialize(user);
            string requestUri = Properties.Resources.UrlToServer + "User/Create";
            var responseMessage = await client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = await responseMessage.Content.ReadAsStringAsync();
                user = serializer.Deserialize<UserDto>(jsonResult);
            }
            else
            {
                user = null;
            }

            return user;
        }

        public async Task DeleteAsync(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "User/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = await client.SendAsync(request);
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
            UserDto user = null;
            string requestUri = Properties.Resources.UrlToServer + "User/GetUser/" + id;
            var responceMessage = await client.GetAsync(requestUri);
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                user = serializer.Deserialize<UserDto>(jsonResult);
            }
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
            IEnumerable<UserDto> users = new UserDto[0];
            string requestUri = Properties.Resources.UrlToServer + "User/GetAllUsers";
            var responceMessage = await client.GetAsync(requestUri);
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                users = serializer.Deserialize<IEnumerable<UserDto>>(jsonResult);
            }

            return users;
        }

        public async Task UpdateAsync(UserDto user)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
            string jsonRequest = serializer.Serialize(user);
            string requestUri = Properties.Resources.UrlToServer + "User/Update";
            var responseMessage = await client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }
}
