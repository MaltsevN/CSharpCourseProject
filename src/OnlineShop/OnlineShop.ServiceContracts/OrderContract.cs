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

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Create(Order item)
        {
            order.Create(item);
            order.Save();
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            order.Delete(id);
            order.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrder/{id}")]
        public Order GetItem(string id)
        {
            return order.GetItem(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrders")]
        public IEnumerable<Order> GetItemsList()
        {
            return order.GetItemsList();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(Order item)
        {
            order.Update(item);
            order.Save();
        }
    }
}
