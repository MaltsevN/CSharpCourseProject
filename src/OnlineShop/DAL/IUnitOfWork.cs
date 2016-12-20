using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
    }
}
