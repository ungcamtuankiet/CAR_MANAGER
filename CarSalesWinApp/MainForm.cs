using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using BusinessObject;
namespace CarSalesWinApp
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm;
        private CarsForm carForm;
        private ManufacturersForm manuForm;
        private CustomerForm customerForm;
        private InvoicesForm invoiceForm;
        private InvoiceCreateForm invoiceCreateForm;
        private StatisticForm statisticForm;
        public EmployeeAccountObject Account { set; get; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.menuStrip1.AllowMerge = false;
            this.WindowState = FormWindowState.Maximized;
            HideLoginMenu();
            OpenLoginForm();
        }

        private void stripMenuLogin_Click(object sender, EventArgs e)
        {
            OpenLoginForm();
        }

        public void OpenLoginForm()
        {
            if (loginForm == null || (loginForm != null && loginForm.IsDisposed))
            {
                loginForm = new LoginForm();
            }
            ShowForm(loginForm);
        }
        private void stripMenuCar_Click(object sender, EventArgs e)
        {
            OpenCarForm();
        }
        public void OpenCarForm()
        {
            if (carForm == null || (carForm != null && carForm.IsDisposed))
            {
                carForm = new CarsForm(Account);
            }
            ShowForm(carForm);
        }

        private void stripMenuManu_Click(object sender, EventArgs e)
        {
            OpenManuForm();
        }
        public void OpenManuForm()
        {
            if (manuForm == null || (manuForm != null && manuForm.IsDisposed))
            {
                manuForm = new ManufacturersForm(Account);
            }
            ShowForm(manuForm);
        }

        private void stripMenuCustomer_Click(object sender, EventArgs e)
        {
            OpenCustomerForm();
        }

        public void OpenCustomerForm()
        {
            if (customerForm == null || (customerForm != null && customerForm.IsDisposed))
            {
                customerForm = new CustomerForm(Account);
            }
            ShowForm(customerForm);
        }

        private void stripMenuCustomerInvoice_Click(object sender, EventArgs e)
        {
            OpenInvoiceForm();
        }
        public void OpenInvoiceForm()
        {
            if (invoiceForm == null || (invoiceForm != null && invoiceForm.IsDisposed))
            {
                invoiceForm = new InvoicesForm(Account);
            }
            ShowForm(invoiceForm);
        }
        private void stripMenuNewInvoice_Click(object sender, EventArgs e)
        {
            OpenInvoiceCreateForm();
        }
        public void OpenInvoiceCreateForm()
        {
            if (invoiceCreateForm == null || (invoiceCreateForm != null && invoiceCreateForm.IsDisposed))
            {
                invoiceCreateForm = new InvoiceCreateForm(Account);
            }
            ShowForm(invoiceCreateForm);
        }
        private void stripMenuCreate_Click(object sender, EventArgs e)
        {
            OpenStatisticForm();
        }
        public void OpenStatisticForm()
        {
            if (statisticForm == null || (statisticForm != null && statisticForm.IsDisposed))
            {
                statisticForm = new StatisticForm(Account);
            }
            ShowForm(statisticForm);
        }
        public void HideLoginMenu()
        {
            if (Account == null)
            {
                stripMenuLogin.Visible = true;
                stripMenuLogout.Visible = false;
                stripMenuManage.Visible = false;
                stripMenuStatistic.Visible = false;
            }
            else
            {
                stripMenuLogin.Visible = false;
                stripMenuLogout.Visible = true;
                stripMenuManage.Visible = true;
                stripMenuStatistic.Visible = true;
            }
        }

        private void CloseAllWindow()
        {
            foreach (var childForm in this.MdiChildren)
            {
                if (childForm != null) childForm.Close();
            }
        }

        private void stripMenuLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to Logout ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Account = null;
                CloseAllWindow();
                HideLoginMenu();
                OpenLoginForm();
            }
        }

        private void stripMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to Exit ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void ShowForm(Form form)
        {
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }
    }
}
