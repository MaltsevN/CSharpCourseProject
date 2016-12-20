using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainModel;
using DAL;

namespace OnlineShop.DAL.EntityFramework
{
    class ProductRepository: IRepository<Product>
    {
        private AppContext appContext;
        public ProductRepository()
        {
            appContext = new AppContext();
        }

        public ProductRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public void Create(Product item)
        {
            appContext.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = appContext.Products.Find(id);
            if (product != null)
                appContext.Products.Remove(product);
        }

        public Product GetItem(int id)
        {
            return appContext.Products.Find(id);
        }

        public IEnumerable<Product> GetItemsList()
        {
            return appContext.Products;
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public void Update(Product item)
        {
            appContext.Entry<Product>(item).State = EntityState.Modified;
        }

    }
}
