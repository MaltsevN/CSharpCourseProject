using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    [DataContract]
    public enum RankDto
    {
        [EnumMember]
        Client,

        [EnumMember]
        Admin,

        [EnumMember]
        HeadAdmin
    }
}
