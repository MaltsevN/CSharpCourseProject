using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime PlacingDate { get; set; }

        [DataMember]
        public Status Status { get; set; }

        [DataMember]
        public List<OrderItem> Items { get; set; }

        [DataMember]
        public User User { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

    }
}
