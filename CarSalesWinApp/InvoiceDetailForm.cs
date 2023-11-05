using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSalesWinApp
{
    public partial class InvoiceDetailForm : Form
    {
        private ICarRepository carRepo;
        private IInvoiceRepository invoiceRepo;
        private List<InvoiceDetailObject> detailList;
        public InvoiceDetailForm(EmployeeAccountObject account, Guid invoiceID)
        {
            if (account != null)
            {
                carRepo = new CarRepository();
                invoiceRepo = new InvoiceRepository();
                detailList = invoiceRepo.GetInvoiceDetailList(invoiceID);
                InitializeComponent();
            }
        }

        private void InvoiceDetailForm_Load(object sender, EventArgs e)
        {
            LoadDetailList();
        }

        private void LoadDetailList()
        {
            var car = carRepo.GetCarList();
            var list = (from detail in detailList
                        join c in car on detail.CarId equals c.CarId
                        select new { c.CarId, c.CarModel, c.CarName, detail.Quantity, detail.CurrentPrice, Total = detail.Quantity * detail.CurrentPrice }).ToList();
            gridDetail.DataSource = null;
            gridDetail.DataSource = list;
            CountTotal();
        }
        private void CountTotal()
        {
            var detailValues = detailList.ToList();
            var total = detailValues.Select(d => new { Total = d.CurrentPrice * d.Quantity })
                                    .Sum(d => d.Total);
            lblTotal.Text += " " + total;
        }
    }
}
