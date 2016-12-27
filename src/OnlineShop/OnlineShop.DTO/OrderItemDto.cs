using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    [DataContract]
    public class OrderItemDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Quantity { get; set; }
        
        [DataMember]
        public ProductDto Product { get; set; }

        [DataMember]
        public int OrderId { get; set; }
    }
}
