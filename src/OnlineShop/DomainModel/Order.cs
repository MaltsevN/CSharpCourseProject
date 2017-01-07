using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Order
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime PlacingDate { get; set; }
        
        public Status Status { get; set; }
        
        public virtual List<OrderItem> Items { get; set; }
        
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

    }
}
