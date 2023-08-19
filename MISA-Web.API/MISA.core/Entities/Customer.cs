using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
