using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainModel;


namespace DAL
{
        public class OrderRepository<T> : IRepository<T> where T : class
        {
            DbContext _context;
            DbSet<T> _dbSet;

            public OrderRepository(DbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public void Create(T item)
            {
                _dbSet.Add(item);

            }
            public void Update(T item)
            {
                _context.Entry(item).State = EntityState.Modified;

            }

            public void Delete(int id)
            {
                T item = _dbSet.Find(id);
                if (item != null) _dbSet.Remove(item);
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public T GetItem(int id)
            {
                return _dbSet.Find(id);
            }

            public IEnumerable<T> GetItemsList()
            {
                return _dbSet.AsNoTracking().ToList();
            }
    }
}
