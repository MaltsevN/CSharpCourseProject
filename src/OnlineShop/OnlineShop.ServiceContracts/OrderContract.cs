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
        IRepository<Order> order;

        public OrderContract(IRepository<Order> order)
        {
            this.order = order;
        }
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Create", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Create(Order item)
        {
            order.Create(item);
            order.Save();
        }
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Delete", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Delete(int id)
        {
            order.Delete(id);
            order.Save();
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetItem/{id}", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public Order GetItem(string id)
        {
            return order.GetItem(Convert.ToInt32(id));
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetItemsList", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public IEnumerable<Order> GetItemsList()
        {
            return order.GetItemsList();
        }
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Update", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Update(Order item)
        {
            order.Update(item);
            order.Save();
        }
    }
}
