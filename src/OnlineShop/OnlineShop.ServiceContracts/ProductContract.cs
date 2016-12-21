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
        IRepository<Product> productRepository;
        
        public ProductContract(IUnitOfWork unitOfWork)
        {
            this.productRepository = unitOfWork.ProductRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public Product Create(Product item)
        {
            Product product = productRepository.Create(item);
            productRepository.Save();
            return product;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            productRepository.Delete(id);
            productRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetProduct/{id}")]
        public Product GetItem(string id)
        {
            return productRepository.GetItem(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllProducts")]
        public IEnumerable<Product> GetItemsList()
        {
            return productRepository.GetItemsList();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(Product item)
        {
            productRepository.Update(item);
            productRepository.Save();
        }
    }
}
