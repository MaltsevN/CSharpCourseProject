using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public interface IProductManager
    {
        ProductDto Create(ProductDto item);
        void Delete(int id);
        ProductDto GetProduct(int id);
        IEnumerable<ProductDto> GetAllProducts();
        void Update(ProductDto item);
    }
}
