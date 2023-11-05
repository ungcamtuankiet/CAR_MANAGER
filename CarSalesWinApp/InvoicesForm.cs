using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject;
using DataAccess.Repository;

namespace CarSalesWinApp
{
    public partial class InvoicesForm : Form
    {
        private ICustomerRepository customerRepo;
        private IInvoiceRepository invoiceRepo;
        private List<InvoiceObject> invoiceList;
        private EmployeeAccountObject account;
        public InvoicesForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                this.account = account;
                customerRepo = new CustomerRepository();
                invoiceRepo = new InvoiceRepository();
                InitializeComponent();
            }
        }

        private void InvoicesForm_Load(object sender, EventArgs e)
        {
            ReloadInvoiceList();
        }

        public void ReloadInvoiceList()
        {
            invoiceList = invoiceRepo.GetInvoiceList();
            LoadInvoiceList(invoiceList);
        }

        private void LoadInvoiceList(IEnumerable<InvoiceObject> list)
        {
            list = list.OrderBy(i => i.CreateDate).ToList();
            gridInvoice.DataSource = null;
            gridInvoice.DataSource = list;
        }

        private void stripMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            Search(search);
        }

        private void Search(string search = "")
        {
            var customerList = customerRepo.GetCustomerList();
            var list = (from i in invoiceList
                          join c in customerList on i.CustomerID equals c.IdentityNumber
                          where i.CustomerID.Equals(search) ||
                                c.FullName.ToLower().Contains(search) ||
                                i.InvoiceID.ToString().ToLower().Equals(search)
                          select i).ToList();
            LoadInvoiceList(list);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ReloadInvoiceList();
            txtSearch.Clear();
        }

        private void InvoicesForm_Activated(object sender, EventArgs e)
        {
            ReloadInvoiceList();
        }

        private void gridInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1 && e.ColumnIndex > -1)
            {
                Guid invoiceID = (Guid)gridInvoice.Rows[row].Cells["InvoiceID"].Value;
                InvoiceDetailForm detailForm = new InvoiceDetailForm(account, invoiceID);
                detailForm.ShowDialog();
            }
        }
    }
}
