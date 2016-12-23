using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.Client.Services
{
    interface IOrderItemService
    {
        OrderItem Create(OrderItem order);
        void Delete(int id);
        OrderItem GetOrderItem(int id);
        IEnumerable<OrderItem> GetOrderItems();
        void Update(OrderItem orderItem);
    }
}
