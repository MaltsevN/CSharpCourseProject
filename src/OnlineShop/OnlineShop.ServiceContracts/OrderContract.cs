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
    public class OrderContract: IOrderContract
    {
        private readonly IOrderManager orderManager;

        public OrderContract(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public OrderDto Create(OrderDto item)
        {
            return orderManager.Create(item);
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderManager.Delete(id);
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrder/{id}")]
        public OrderDto GetItem(string id)
        {
            return orderManager.GetOrder(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrders")]
        public IEnumerable<OrderDto> GetItemsList()
        {
            return orderManager.GetAllOrders();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(OrderDto item)
        {
            orderManager.Update(item);
        }
    }
}
