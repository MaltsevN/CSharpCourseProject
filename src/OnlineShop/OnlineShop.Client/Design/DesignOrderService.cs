using OnlineShop.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.Client.Design
{
    class DesignOrderService : IOrderService
    {
        static List<Order> orders = new List<Order>();

        static DesignOrderService()
        {
            DesignUserService userService = new DesignUserService();
            User customer1 = userService.GetUser(0);
            User customer2 = userService.GetUser(1);

            orders.Add(new Order()
            {
                Id = 0,
                Name = "Food order customer 1",
                PlacingDate = DateTime.UtcNow,
                Status = Status.NotDecorated,
                User = customer1
            });

            orders.Add(new Order()
            {
                Id = 1,
                Name = "Drink order  customer 1",
                PlacingDate = DateTime.UtcNow,
                Status = Status.Processing,
                User = customer1
            });

            orders.Add(new Order()
            {
                Id = 2,
                Name = "Food order  customer 2",
                PlacingDate = DateTime.UtcNow,
                Status = Status.NotDecorated,
                User = customer2
            });

            orders.Add(new Order()
            {
                Id = 3,
                Name = "Drink order  customer 2",
                PlacingDate = DateTime.UtcNow,
                Status = Status.Processing,
                User = customer2
            });
        }

        public void Create(Order order)
        {
            order.Id = orders.Count;
            orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = orders.Find(o => o.Id == id);
            if (order != null)
                orders.Remove(order);
        }

        public Order GetOrder(int id)
        {
            return orders.Find(o => o.Id == id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders;
        }

        public void Update(Order order)
        {
            var tempOrder = orders.Find(o => o.Id == order.Id);
            if (tempOrder != null)
            {
                orders.Remove(tempOrder);
            }

            orders.Add(order);
        }
    }
}
