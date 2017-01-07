using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using OnlineShop.DTO;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using OnlineShop.Client.Exceptions;

namespace OnlineShop.Client.Services
{
    class OrderService : IOrderService, IDisposable
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public OrderService(IAuthenticationService authService)
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
        }

        public async Task<OrderDto> CreateAsync(OrderDto order)
        {
            string jsonRequest = serializer.Serialize(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Create";
            try
            {
                var responseMessage = await client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    order = serializer.Deserialize<OrderDto>(jsonResult);
                }
                else
                {
                    order = null;
                }

                return order;
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

        public async Task DeleteAsync(int id)
        {
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "Order/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };

            try
            {
                var responseMessage = await client.SendAsync(request);
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

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            OrderDto order = null;
            string requestUri = Properties.Resources.UrlToServer + "Order/GetOrder/" + id;
            try
            {
                var responceMessage = await client.GetAsync(requestUri);
                if (responceMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                    order = serializer.Deserialize<OrderDto>(jsonResult);
                }
                return order;
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

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            IEnumerable<OrderDto> orders = new OrderDto[0];
            string requestUri = Properties.Resources.UrlToServer + "Order/GetAllOrders";
            try
            {
                var responceMessage = await client.GetAsync(requestUri);
                if (responceMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                    orders = serializer.Deserialize<IEnumerable<OrderDto>>(jsonResult);
                }

                return orders;
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

        public async Task UpdateAsync(OrderDto order)
        {
            string jsonRequest = serializer.Serialize(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Update";
            try
            {
                var responseMessage = await client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
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

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
