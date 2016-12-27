using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
