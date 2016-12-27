using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    [DataContract]
    public enum StatusDto
    {
        [EnumMember]
        NotDecorated,

        [EnumMember]
        Cancelled,

        [EnumMember]
        Confirmed,

        [EnumMember]
        Processing
    }
}
