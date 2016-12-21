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
        IRepository<Order> orderRepository;

        public OrderContract(IUnitOfWork unitOfWork)
        {
            this.orderRepository = unitOfWork.OrderRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public Order Create(Order item)
        {
            Order order = orderRepository.Create(item);
            orderRepository.Save();
            return order;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrder/{id}")]
        public Order GetItem(string id)
        {
            return orderRepository.GetItem(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrders")]
        public IEnumerable<Order> GetItemsList()
        {
            return orderRepository.GetItemsList();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(Order item)
        {
            orderRepository.Update(item);
            orderRepository.Save();
        }
    }
}
