using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using OnlineShop.DTO;
using AutoMapper;

namespace OnlineShop.ServiceContracts
{
    public class OrderItemContract : IOrderItemContract
    {
        IRepository<OrderItem> orderItemRepository;
        IRepository<Order> orderRepository;
        IRepository<Product> productRepository;

        public OrderItemContract(IUnitOfWork unitOfWork)
        {
            this.orderItemRepository = unitOfWork.OrderItemRepository;
            this.orderRepository = unitOfWork.OrderRepository;
            this.productRepository = unitOfWork.ProductRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public OrderItemDto Create(OrderItemDto item)
        {
            OrderItem convertedOrder = Mapper.Map<OrderItemDto, OrderItem>(item);
            convertedOrder.Order = orderRepository.GetItem(convertedOrder.OrderId);
            convertedOrder.Product = productRepository.GetItem(convertedOrder.ProductId);
            OrderItem orderItem = orderItemRepository.Create(convertedOrder);
            orderItemRepository.Save();
            OrderItemDto dto = Mapper.Map<OrderItem, OrderItemDto>(orderItem);
            return dto;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderItemRepository.Delete(id);
            orderItemRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrderItem/{id}")]
        public OrderItemDto GetItem(string id)
        {
            OrderItem orderItem = orderItemRepository.GetItem(Convert.ToInt32(id));
            OrderItemDto dto = Mapper.Map<OrderItem, OrderItemDto>(orderItem);
            orderItem = Mapper.Map<OrderItemDto, OrderItem>(dto);
            return dto;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrderItems")]
        public IEnumerable<OrderItemDto> GetItemsList()
        {
            var items = orderItemRepository.GetItemsList();
            IEnumerable<OrderItemDto> dtos = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDto>>(items);
            return dtos;
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(OrderItemDto item)
        {
            OrderItem convertedOrder = Mapper.Map<OrderItemDto, OrderItem>(item);
            convertedOrder.Order = orderRepository.GetItem(convertedOrder.OrderId);
            convertedOrder.Product = productRepository.GetItem(convertedOrder.ProductId);
            orderItemRepository.Update(convertedOrder);
            orderItemRepository.Save();
        }
    }
}
