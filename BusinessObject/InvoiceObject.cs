using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class InvoiceObject
    {
        public Guid InvoiceID { set; get; }
        public string CustomerID { set; get; }
        public string EmployeeID { set; get; }
        public DateTime CreateDate { set; get; }
        public decimal Total { set; get; }
    }
}
