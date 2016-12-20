using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.DAL.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Order> OrderRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
