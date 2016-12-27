using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IOrderService
    {
        OrderDto Create(OrderDto order);
        void Delete(int id);
        OrderDto GetOrder(int id);
        IEnumerable<OrderDto> GetOrders();
        void Update(OrderDto order);
    }
}
