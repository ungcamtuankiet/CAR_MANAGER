using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private InvoiceDAO instance = InvoiceDAO.Instance;
        public List<InvoiceObject> GetInvoiceList() => instance.GetInvoiceList();
        public List<InvoiceDetailObject> GetAllDetailList() => instance.GetAllDetailList();
        public List<InvoiceDetailObject> GetInvoiceDetailList(Guid invoiceID) => instance.GetInvoiceDetailList(invoiceID);
        public bool AddInvoice(InvoiceObject invoice, Dictionary<int, InvoiceDetailObject> detailList) => instance.AddInvoice(invoice, detailList);
    }
}
