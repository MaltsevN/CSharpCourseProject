using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.Client.Services
{
    interface IProductService
    {
        Product Create(Product product);
        void Delete(int id);
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts();
        void Update(Product product);
    }
}
