using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using OnlineShop.DTO;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;

namespace OnlineShop.Client.Services
{
    class OrderService : IOrderService
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
            var responseMessage = await client.SendAsync(request);
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            OrderDto order = null;
            string requestUri = Properties.Resources.UrlToServer + "Order/GetOrder/" + id;
            var responceMessage = await client.GetAsync(requestUri);
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                order = serializer.Deserialize<OrderDto>(jsonResult);
            }
            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            IEnumerable<OrderDto> orders = new OrderDto[0];
            string requestUri = Properties.Resources.UrlToServer + "Order/GetAllOrders";
            var responceMessage = await client.GetAsync(requestUri);
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = await responceMessage.Content.ReadAsStringAsync();
                orders = serializer.Deserialize<IEnumerable<OrderDto>>(jsonResult);
            }

            return orders;
        }

        public async Task UpdateAsync(OrderDto order)
        {
            string jsonRequest = serializer.Serialize(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Update";
            var responseMessage = await client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }
}
