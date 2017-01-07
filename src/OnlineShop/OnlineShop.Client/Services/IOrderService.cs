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
        Task<OrderDto> CreateAsync(OrderDto order);
        Task DeleteAsync(int id);
        Task<OrderDto> GetOrderAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task UpdateAsync(OrderDto order);
    }
}
