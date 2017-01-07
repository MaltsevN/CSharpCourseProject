using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public interface IOrderManager
    {
        OrderDto Create(OrderDto item);
        void Delete(int id);
        OrderDto GetOrder(int id);
        IEnumerable<OrderDto> GetAllOrders();
        void Update(OrderDto item);
    }
}
