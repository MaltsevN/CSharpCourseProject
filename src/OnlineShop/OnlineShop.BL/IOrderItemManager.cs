using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public interface IOrderItemManager
    {
        OrderItemDto Create(OrderItemDto item);
        void Delete(int id);
        OrderItemDto GetOrderItem(int id);
        IEnumerable<OrderItemDto> GetAllOrderItems();
        void Update(OrderItemDto item);
    }
}
