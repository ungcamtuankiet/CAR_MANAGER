using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class InvoiceDetailObject
    {
        public Guid InvoiceID { set; get; }
        public int CarId { set; get; }
        public int Quantity { set; get; }
        public decimal CurrentPrice { set; get; }
    }
}
