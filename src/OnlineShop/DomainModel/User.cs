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

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string Login { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        public Rank Rank { get; set; }
        
        public virtual List<Order> Orders { get; set; }

        public virtual AuthenticationToken AuthenticationToken { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

    }
}
