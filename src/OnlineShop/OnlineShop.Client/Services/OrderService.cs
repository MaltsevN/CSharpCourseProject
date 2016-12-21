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
    class OrderService : IOrderService
    {
        private readonly HttpClient client;

        public OrderService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            client = new HttpClient();
        }

        public Order Create(Order order)
        {
            string jsonRequest = JsonConvert.SerializeObject(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(jsonResult);
            }else
            {
                order = null;
            }

            return order;
        }

        public void Delete(int id)
        {
            string jsonRequest = JsonConvert.SerializeObject(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "Order/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            string jsonRequest = JsonConvert.SerializeObject(order);
            string requestUri = Properties.Resources.UrlToServer + "Order/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
