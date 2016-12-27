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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductDto GetProduct(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
