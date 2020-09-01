using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Domain;

namespace Vidly.Models
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set;   }
    }
}
