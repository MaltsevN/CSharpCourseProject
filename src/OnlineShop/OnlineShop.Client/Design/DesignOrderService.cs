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

            Order order1 = new Order()
            {
                Id = 0,
                Name = "Food order customer 1",
                PlacingDate = DateTime.UtcNow,
                Status = Status.NotDecorated,
                User = customer1
            };

            order1.User.Orders.Add(order1);

            Order order2 = new Order()
            {
                Id = 1,
                Name = "Drink order  customer 1",
                PlacingDate = DateTime.UtcNow,
                Status = Status.Processing,
                User = customer1
            };

            order2.User.Orders.Add(order2);

            Order order3 = new Order()
            {
                Id = 2,
                Name = "Food order  customer 2",
                PlacingDate = DateTime.UtcNow,
                Status = Status.NotDecorated,
                User = customer2
            };

            order3.User.Orders.Add(order3);

            Order order4 = new Order()
            {
                Id = 3,
                Name = "Drink order  customer 2",
                PlacingDate = DateTime.UtcNow,
                Status = Status.Processing,
                User = customer2
            };

            order4.User.Orders.Add(order4);

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
            orders.Add(order4);
        }

        public Order Create(Order order)
        {
            order.Id = orders.Count;
            orders.Add(order);
            order.User.Orders.Add(order);
            return order;
        }

        public void Delete(int id)
        {
            var order = orders.Find(o => o.Id == id);
            if (order != null)
            {
                orders.Remove(order);
                order.User.Orders.Remove(order);
            }
                
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
                order.User.Orders.Remove(order);
            }

            orders.Add(order);
            order.User.Orders.Add(order);
        }
    }
}
