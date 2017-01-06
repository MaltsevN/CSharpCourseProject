using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using OnlineShop.DTO;
using OnlineShop.BL;

namespace OnlineShop.ServiceContracts
{
    public class ProductContract: IProductContract
    {
        private readonly IProductManager productManager;

        public ProductContract(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public ProductDto Create(ProductDto item)
        {
            return productManager.Create(item);
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            productManager.Delete(id);
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetProduct/{id}")]
        public ProductDto GetItem(string id)
        {
            return productManager.GetProduct(Convert.ToInt32(id));
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllProducts")]
        public IEnumerable<ProductDto> GetItemsList()
        {
            return productManager.GetAllProducts();
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(ProductDto item)
        {
            productManager.Update(item);
        }
    }
}
