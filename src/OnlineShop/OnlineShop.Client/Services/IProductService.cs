using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    interface IProductService
    {
        Task<ProductDto> CreateAsync(ProductDto product);
        Task DeleteAsync(int id);
        Task<ProductDto> GetProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task UpdateAsync(ProductDto product);
    }
}
