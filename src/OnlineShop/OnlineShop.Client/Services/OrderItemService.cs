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
    class OrderItemService : IOrderItemService
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public OrderItemService()
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
        }


        public OrderItemDto Create(OrderItemDto orderItem)
        {
            string jsonRequest = serializer.Serialize(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                orderItem = serializer.Deserialize<OrderItemDto>(jsonResult);
            }
            else
            {
                orderItem = null;
            }

            return orderItem;
        }

        public void Delete(int id)
        {
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
        }

        public OrderItemDto GetOrderItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItemDto> GetOrderItems()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItemDto orderItem)
        {
            string jsonRequest = serializer.Serialize(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
