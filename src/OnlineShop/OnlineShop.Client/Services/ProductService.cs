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
    class ProductService : IProductService
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer serializer;

        public ProductService()
        {
            client = new HttpClient();
            serializer = new JavaScriptSerializer();
        }

        public ProductDto Create(ProductDto product)
        {
            string jsonRequest = serializer.Serialize(product);
            string requestUri = Properties.Resources.UrlToServer + "Product/Create";
            var responseMessage = client.PostAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().Result;
                product = serializer.Deserialize<ProductDto>(jsonResult);
            }
            else
            {
                product = null;
            }

            return product;
        }

        public void Delete(int id)
        {
            string jsonRequest = serializer.Serialize(id.ToString());
            string requestUri = Properties.Resources.UrlToServer + "Product/Delete";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            var responseMessage = client.SendAsync(request).Result;
        }

        public ProductDto GetProduct(int id)
        {
            ProductDto product = null;
            string requestUri = Properties.Resources.UrlToServer + "Product/GetProduct/" + id;
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                product = serializer.Deserialize<ProductDto>(jsonResult);
            }
            return product;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            IEnumerable<ProductDto> products = new ProductDto[0];
            string requestUri = Properties.Resources.UrlToServer + "Product/GetAllProducts";
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                products = serializer.Deserialize<IEnumerable<ProductDto>>(jsonResult);
            }

            return products;
        }

        public void Update(ProductDto product)
        {
            string jsonRequest = serializer.Serialize(product);
            string requestUri = Properties.Resources.UrlToServer + "Product/Update";
            var responseMessage = client.PutAsync(requestUri, new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
        }
    }
}
