using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto user);
        Task DeleteAsync(int id);
        Task<UserDto> GetUserAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task UpdateAsync(UserDto user);
    }
}
