using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IOrderItemService
    {
        OrderItemDto Create(OrderItemDto order);
        void Delete(int id);
        OrderItemDto GetOrderItem(int id);
        IEnumerable<OrderItemDto> GetOrderItems();
        void Update(OrderItemDto orderItem);
    }
}
