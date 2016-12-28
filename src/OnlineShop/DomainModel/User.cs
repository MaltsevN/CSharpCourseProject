using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [MaxLength(25)]
        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public Rank Rank { get; set; }
        
        public virtual List<Order> Orders { get; set; }

        public AuthenticationToken AuthenticationToken { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

    }
}
