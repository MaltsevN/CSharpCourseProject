using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IOrderItemService
    {
        Task<OrderItemDto> CreateAsync(OrderItemDto order);
        Task DeleteAsync(int id);
        Task<OrderItemDto> GetOrderItemAsync(int id);
        Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync();
        Task UpdateAsync(OrderItemDto orderItem);
    }
}
