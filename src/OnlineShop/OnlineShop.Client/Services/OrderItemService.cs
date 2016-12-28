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
            OrderItemDto orderItem = null;
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/GetOrderItem/" + id;
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                orderItem = serializer.Deserialize<OrderItemDto>(jsonResult);
            }
            return orderItem;
        }

        public IEnumerable<OrderItemDto> GetOrderItems()
        {
            IEnumerable<OrderItemDto> orderItems = new OrderItemDto[0];
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/GetAllOrderItems";
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                orderItems = serializer.Deserialize<IEnumerable<OrderItemDto>>(jsonResult);
            }

            return orderItems;
        }

        public void Update(OrderItemDto orderItem)
        {
            string jsonRequest = serializer.Serialize(orderItem);
            string requestUri = Properties.Resources.UrlToServer + "OrderItem/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
