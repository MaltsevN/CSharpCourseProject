using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IOrderService
    {
        void Create(Order order);
        void Delete(int id);
        Order GetOrder(int id);
        IEnumerable<Order> GetOrders();
        void Update(Order order);
    }
}
