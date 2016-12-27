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
    public class OrderContract: IOrderContract
    {
        IRepository<Order> orderRepository;
        IRepository<User> userRepository;

        public OrderContract(IUnitOfWork unitOfWork)
        {
            this.orderRepository = unitOfWork.OrderRepository;
            this.userRepository = unitOfWork.UserRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public OrderDto Create(OrderDto item)
        {
            Order convertedOrder = Mapper.Map<OrderDto, Order>(item);
            convertedOrder.User = userRepository.GetItem(convertedOrder.UserId);
            Order order = orderRepository.Create(convertedOrder);
            orderRepository.Save();
            OrderDto dto = Mapper.Map<Order, OrderDto>(order);
            return dto;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetOrder/{id}")]
        public OrderDto GetItem(string id)
        {
            var order = orderRepository.GetItem(Convert.ToInt32(id));
            OrderDto dto = Mapper.Map<Order, OrderDto>(order);
            order = Mapper.Map<OrderDto, Order>(dto);
            return dto;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllOrders")]
        public IEnumerable<OrderDto> GetItemsList()
        {
            var items = orderRepository.GetItemsList();
            IEnumerable<OrderDto> dtos = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(items);
            return dtos;
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(OrderDto item)
        {
            Order convertedOrder = Mapper.Map<OrderDto, Order>(item);
            convertedOrder.User = userRepository.GetItem(item.User.Id);
            orderRepository.Update(convertedOrder);
            orderRepository.Save();
        }
    }
}
