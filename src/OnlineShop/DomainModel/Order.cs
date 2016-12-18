using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PlacingDate { get; set; }

        public virtual Status Status { get; set; }

        public List<OrderItem> Items { get; set; }
        public User User { get; set; }

    }
}
