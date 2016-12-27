using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using OnlineShop.DTO;
using System.Web.Script.Serialization;

namespace OnlineShop.Client.Services
{
    class OrderService : IOrderService
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public OrderService()
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
        }

        public OrderDto Create(OrderDto order)
        {
            string jsonRequest = serializer.Serialize(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                order = serializer.Deserialize<OrderDto>(jsonResult);
            }
            else
            {
                order = null;
            }

            return order;
        }

        public void Delete(int id)
        {
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "Order/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
        }

        public OrderDto GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto order)
        {
            string jsonRequest = serializer.Serialize(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
