using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DTO;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Net.NetworkInformation;
using OnlineShop.Client.Exceptions;

namespace OnlineShop.Client.Services
{
    class AuthenticationService : IAuthenticationService
    {
        public AuthenticationTokenDto AuthenticationToken { get; private set; }

        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public AuthenticationService()
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
        }

        public async Task SignIn(string login, string password)
        {
            string jsonRequest = serializer.Serialize(new { login = login, password = password });
            string requestUri = Properties.Resources.UrlToServer + "Account/SignIn";
            try
            {
                var responseMessage = await client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    AuthenticationToken = serializer.Deserialize<AuthenticationTokenDto>(jsonResult);
                }
            }
            catch (HttpRequestException ex)
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    throw new NoInternetConnectionException("No internet connection, please try again later.");
                }
                throw ex;
            }
            
        }
    }
}
