using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace OnlineShop.ServiceContracts
{
    public class ProductContract: IProductContract
    {
        IRepository<Product> product;

        public ProductContract(IRepository<Product> product)
        {
            this.product = product;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Create(Product item)
        {
            product.Create(item);
            product.Save();
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            product.Delete(id);
            product.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetProduct/{id}")]
        public Product GetItem(string id)
        {
            return product.GetItem(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllProducts")]
        public IEnumerable<Product> GetItemsList()
        {
            return product.GetItemsList();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(Product item)
        {
            product.Update(item);
            product.Save();
        }
    }
}
