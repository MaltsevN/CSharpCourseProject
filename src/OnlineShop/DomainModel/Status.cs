using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [DataContract]
    public enum Status
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
