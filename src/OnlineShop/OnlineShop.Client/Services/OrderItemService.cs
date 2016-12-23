using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace OnlineShop.Client.Services
{
    class OrderItemService : IOrderItemService
    {
        private readonly HttpClient client;

        public OrderItemService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            client = new HttpClient();
        }


        public OrderItem Create(OrderItem orderItem)
        {
            string jsonRequest = JsonConvert.SerializeObject(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                orderItem = JsonConvert.DeserializeObject<OrderItem>(jsonResult);
            }
            else
            {
                orderItem = null;
            }

            return orderItem;
        }

        public void Delete(int id)
        {
            string jsonRequest = JsonConvert.SerializeObject(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
        }

        public OrderItem GetOrderItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem orderItem)
        {
            string jsonRequest = JsonConvert.SerializeObject(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
