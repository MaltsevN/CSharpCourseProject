using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using OnlineShop.DTO;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using OnlineShop.Client.Exceptions;

namespace OnlineShop.Client.Services
{
    class OrderItemService : IOrderItemService, IDisposable
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public OrderItemService(IAuthenticationService authService)
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.AuthenticationToken.Login + ":" + authService.AuthenticationToken.Token);
        }


        public async Task<OrderItemDto> CreateAsync(OrderItemDto orderItem)
        {
            string jsonRequest = serializer.Serialize(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Create";
            try
            {
                var responseMessage = await client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    orderItem = serializer.Deserialize<OrderItemDto>(jsonResult);
                }
                else
                {
                    orderItem = null;
                }

                return orderItem;
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
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Delete";
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

        public async Task<OrderItemDto> GetOrderItemAsync(int id)
        {
            OrderItemDto orderItem = null;
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/GetOrderItem/" + id;
            try
            {
                var responceMessage = await client.GetAsync(requestUri);
                if (responceMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                    orderItem = serializer.Deserialize<OrderItemDto>(jsonResult);
                }
                return orderItem;
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

        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync()
        {
            IEnumerable<OrderItemDto> orderItems = new OrderItemDto[0];
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/GetAllOrderItems";
            try
            {
                var responceMessage = await client.GetAsync(requestUri);
                if (responceMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                    orderItems = serializer.Deserialize<IEnumerable<OrderItemDto>>(jsonResult);
                }

                return orderItems;
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

        public async Task UpdateAsync(OrderItemDto orderItem)
        {
            string jsonRequest = serializer.Serialize(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Update";
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
