using AutoMapper;
using DAL;
using DomainModel;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public class OrderManager : IOrderManager
    {
        IRepository<Order> orderRepository;
        IRepository<User> userRepository;

        static OrderManager()
        {
            AutoMapperConfigurator.Configure();
        }

        public OrderManager(IUnitOfWork unitOfWork)
        {
            this.orderRepository = unitOfWork.OrderRepository;
            this.userRepository = unitOfWork.UserRepository;
        }
        
        public OrderDto Create(OrderDto item)
        {
            Order convertedOrder = Mapper.Map<OrderDto, Order>(item);
            Order order = orderRepository.Create(convertedOrder);
            orderRepository.Save();
            OrderDto dto = Mapper.Map<Order, OrderDto>(order);
            return dto;
        }
        
        public void Delete(int id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();
        }

        public OrderDto GetOrder(int id)
        {
            var order = orderRepository.GetItem(id);
            OrderDto dto = Mapper.Map<Order, OrderDto>(order);
            order = Mapper.Map<OrderDto, Order>(dto);
            return dto;
        }
        
        public IEnumerable<OrderDto> GetAllOrders()
        {
            var items = orderRepository.GetItemsList();
            IEnumerable<OrderDto> dtos = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(items);
            return dtos;
        }
        
        public void Update(OrderDto item)
        {
            Order convertedOrder = Mapper.Map<OrderDto, Order>(item);
            orderRepository.Update(convertedOrder);
            orderRepository.Save();
        }
    }
}
