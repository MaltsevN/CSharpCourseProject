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
    public class OrderItemContract : IOrderItemContract
    {
        private readonly IOrderItemManager orderItemManager;

        public OrderItemContract(IOrderItemManager orderItemManager)
        {
            this.orderItemManager = orderItemManager;
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public OrderItemDto Create(OrderItemDto item)
        {
            return orderItemManager.Create(item);
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderItemManager.Delete(id);
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrderItem/{id}")]
        public OrderItemDto GetItem(string id)
        {
            return orderItemManager.GetOrderItem(Convert.ToInt32(id));
        }

        [Authorize(new[] { RankDto.Admin })]
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrderItems")]
        public IEnumerable<OrderItemDto> GetItemsList()
        {
            return orderItemManager.GetAllOrderItems();
        }

        [Authorize(new[] { RankDto.Admin, RankDto.Client })]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(OrderItemDto item)
        {
            orderItemManager.Update(item);
        }
    }
}
