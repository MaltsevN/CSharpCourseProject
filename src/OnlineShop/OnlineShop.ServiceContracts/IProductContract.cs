using System;
using DAL;
using DomainModel;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DTO;

namespace OnlineShop.ServiceContracts
{
    [ServiceContract]
    interface IProductContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        ProductDto Create(ProductDto item);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void Delete(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetProduct/{id}")]
        ProductDto GetItem(string id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllProducts")]
        IEnumerable<ProductDto> GetItemsList();

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void Update(ProductDto item);
    }
}
