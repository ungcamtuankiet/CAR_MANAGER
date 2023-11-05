using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CustomerObject
    {
        public string IdentityNumber { set; get; }
        public string FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Gender { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public bool Status { set; get; }
    }
}
