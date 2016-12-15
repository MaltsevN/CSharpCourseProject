using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ProductPrice Price { get; set; }
    }
}
