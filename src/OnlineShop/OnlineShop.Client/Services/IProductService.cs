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
        ProductDto Create(ProductDto product);
        void Delete(int id);
        ProductDto GetProduct(int id);
        IEnumerable<ProductDto> GetProducts();
        void Update(ProductDto product);
    }
}
