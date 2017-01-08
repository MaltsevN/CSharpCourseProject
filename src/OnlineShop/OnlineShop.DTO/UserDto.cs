using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTO
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public RankDto Rank { get; set; }
        
        [DataMember]
        public List<OrderDto> Orders { get; set; }

        [DataMember]
        public string Password { get; set; }

        public UserDto()
        {
            Orders = new List<OrderDto>();
        }
    }
}
