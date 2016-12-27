using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    [DataContract]
    public class ProductPriceDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public DateTime EffectiveDate { get; set; }
    }
}
