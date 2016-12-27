using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class ProductPrice
    {
        public int Id { get; set; }
        
        public double Price { get; set; }
        
        public DateTime EffectiveDate { get; set; }
    }
}
