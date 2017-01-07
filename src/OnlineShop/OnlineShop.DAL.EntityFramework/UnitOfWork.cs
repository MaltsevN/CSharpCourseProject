using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace OnlineShop.DAL.EntityFramework
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppContext appContext = new AppContext();
        private ProductRepository productRepository;
        private UserRepository userRepository;
        private OrderRepository orderRepository;
        private OrderItemRepository orderItemRepository;

        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(appContext);
                return userRepository;
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(appContext);
                return productRepository;
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(appContext);
                return orderRepository;
            }
        }

        public IRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (orderItemRepository == null)
                    orderItemRepository = new OrderItemRepository(appContext);
                return orderItemRepository;
            }
        }
        
        public void Save()
        {
            appContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    appContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
