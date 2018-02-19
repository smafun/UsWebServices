using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsWebServices.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
