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
    public class OrderItemManager : IOrderItemManager
    {
        IRepository<OrderItem> orderItemRepository;
        IRepository<Order> orderRepository;
        IRepository<Product> productRepository;

        static OrderItemManager()
        {
            AutoMapperConfigurator.Configure();
        }

        public OrderItemManager(IUnitOfWork unitOfWork)
        {
            this.orderItemRepository = unitOfWork.OrderItemRepository;
            this.orderRepository = unitOfWork.OrderRepository;
            this.productRepository = unitOfWork.ProductRepository;
        }
        
        public OrderItemDto Create(OrderItemDto item)
        {
            OrderItem convertedOrder = Mapper.Map<OrderItemDto, OrderItem>(item);
            OrderItem orderItem = orderItemRepository.Create(convertedOrder);
            orderItemRepository.Save();
            OrderItemDto dto = Mapper.Map<OrderItem, OrderItemDto>(orderItem);
            return dto;
        }

        public void Delete(int id)
        {
            orderItemRepository.Delete(id);
            orderItemRepository.Save();
        }
        
        public OrderItemDto GetOrderItem(int id)
        {
            OrderItem orderItem = orderItemRepository.GetItem(id);
            OrderItemDto dto = Mapper.Map<OrderItem, OrderItemDto>(orderItem);
            orderItem = Mapper.Map<OrderItemDto, OrderItem>(dto);
            return dto;
        }
        
        public IEnumerable<OrderItemDto> GetAllOrderItems()
        {
            var items = orderItemRepository.GetItemsList();
            IEnumerable<OrderItemDto> dtos = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDto>>(items);
            return dtos;
        }
        
        public void Update(OrderItemDto item)
        {
            OrderItem convertedOrder = Mapper.Map<OrderItemDto, OrderItem>(item);
            orderItemRepository.Update(convertedOrder);
            orderItemRepository.Save();
        }
    }
}
