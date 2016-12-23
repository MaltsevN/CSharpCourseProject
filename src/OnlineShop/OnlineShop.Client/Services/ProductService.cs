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
    class ProductService : IProductService
    {
        private readonly HttpClient client;

        public ProductService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            client = new HttpClient();
        }

        public Product Create(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> products = new Product[0];
            string requestUri = Properties.Resources.UrlToServer + "Product/GetAllProducts";
            var responceMessage = client.GetAsync(requestUri).Result;
            if (responceMessage.IsSuccessStatusCode)
            {
                string jsonResult = responceMessage.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonResult);
            }

            return products;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
