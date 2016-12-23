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
    public class OrderItemContract : IOrderItemContract
    {
        IRepository<OrderItem> orderItemRepository;

        public OrderItemContract(IUnitOfWork unitOfWork)
        {
            this.orderItemRepository = unitOfWork.OrderItemRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public OrderItem Create(OrderItem item)
        {
            OrderItem orderItem = orderItemRepository.Create(item);
            orderItemRepository.Save();
            return orderItem;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderItemRepository.Delete(id);
            orderItemRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrderItem/{id}")]
        public OrderItem GetItem(string id)
        {
            return orderItemRepository.GetItem(Convert.ToInt32(id));
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrderItems")]
        public IEnumerable<OrderItem> GetItemsList()
        {
            return orderItemRepository.GetItemsList();
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(OrderItem item)
        {
            orderItemRepository.Update(item);
            orderItemRepository.Save();
        }
    }
}
