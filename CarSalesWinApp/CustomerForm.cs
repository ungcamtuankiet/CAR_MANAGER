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
    public partial class CustomerForm : Form
    {
        EmployeeAccountObject account;
        private ICustomerRepository customerRepo;
        private IEnumerable<CustomerObject> customerList;
        public CustomerForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                this.account = account;
                customerRepo = new CustomerRepository();
                InitializeComponent();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            ReloadCustomerList();
        }

        public void ReloadCustomerList()
        {
            customerList = customerRepo.GetCustomerList();
            LoadCustomerList(customerList);
        }

        private void LoadCustomerList(IEnumerable<CustomerObject> list)
        {
            gridCustomer.DataSource = null;
            gridCustomer.DataSource = list;
            HideDeleteMenu();
        }

        private void HideDeleteMenu()
        {
            if (gridCustomer.Rows.Count > 0)
            {
                stripMenuDelete.Enabled = true;
            }
            else
            {
                stripMenuDelete.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ReloadCustomerList();
            cmbStatus.SelectedIndex = 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            int status = cmbStatus.SelectedIndex > -1 ? cmbStatus.SelectedIndex : 2;
            Search(search, status);
        }

        private void Search(string search = "", int status = 2)
        {
            var list = from c in customerList
                       where c.IdentityNumber.ToString().Equals(search) ||
                             c.FullName.ToLower().Contains(search) ||
                             c.PhoneNumber.Equals(search)
                       select c;
            if (status == 0) list = list.Where(c => c.Status == false);
            else if (status == 1) list = list.Where(c => c.Status == true);
            LoadCustomerList(list.ToList());
        }

        private void stripMenuDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deactive thoes customer(s) ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                bool valid = true;
                foreach (DataGridViewRow row in gridCustomer.SelectedRows)
                {
                    valid = customerRepo.DeleteCustomer(row.Cells["IdentityNumber"].Value.ToString());
                }
                if (valid)
                {
                    MessageBox.Show("Change Saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ReloadCustomerList();
            }
        }

        private void gridCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if ( row > -1 && e.ColumnIndex > -1)
            {
                var customer = GetCustomerFromCell(row);
                CustomerInsertUpdateForm updateForm = new(account, customer);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadCustomerList();
                }
            }
        }

        private CustomerObject GetCustomerFromCell (int row)
        {
            var cell = gridCustomer.Rows[row].Cells;
            var customer = new CustomerObject()
            {
                IdentityNumber = cell["IdentityNumber"].Value.ToString(),
                FullName = cell["FullName"].Value.ToString(),
                DateOfBirth = Convert.ToDateTime(cell["DateOfBirth"].Value),
                Gender = cell["Gender"].Value.ToString(),
                PhoneNumber = cell["PhoneNumber"].Value.ToString(),
                Address = cell["Address"].Value.ToString(),
                Status = (bool) cell["Status"].Value
            };
            return customer;
        }

        private void stripMenuNew_Click(object sender, EventArgs e)
        {
            CustomerInsertUpdateForm insertForm = new(account);
            if (insertForm.ShowDialog() == DialogResult.OK)
            {
                ReloadCustomerList();
            }
        }

        private void CustomerForm_Activated(object sender, EventArgs e)
        {
            ReloadCustomerList();
        }
    }
}
