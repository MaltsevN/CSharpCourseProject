using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        [MaxLength(25)]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Rank Rank { get; set; }

        [DataMember]
        public virtual List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

    }
}
