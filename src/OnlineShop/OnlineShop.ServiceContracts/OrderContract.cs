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
    public class OrderContract: IOrderContract
    {
        OrderContract(IRepository<Order> order)
        {

        }
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Create", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Create(Order item)
        {
             
        }
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Delete", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Delete(int id)
        {

        }
         public Order GetItem(int id)
        {
            
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetItemsList", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public IEnumerable<Order> GetItemsList()
        {

        }
        [WebInvoke(Method = "UPDATE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Update", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Update(Order item)
        {

        }
    }
}
