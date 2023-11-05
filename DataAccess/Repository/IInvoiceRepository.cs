using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface IInvoiceRepository
    {
        List<InvoiceObject> GetInvoiceList();
        List<InvoiceDetailObject> GetInvoiceDetailList(Guid invoiceID);
        List<InvoiceDetailObject> GetAllDetailList();
        bool AddInvoice(InvoiceObject invoice, Dictionary<int, InvoiceDetailObject> detailList);
    }
}
