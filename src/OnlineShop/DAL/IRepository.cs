using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        T Create(T item);
        void Delete(int id);
        T GetItem(int id);
        IEnumerable<T> GetItemsList();
        void Save();
        void Update(T item);
    }
}
