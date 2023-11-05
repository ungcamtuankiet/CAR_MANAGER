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
    public partial class LoginForm : Form
    {
        private IAccountRepository accountRepo;
        public LoginForm()
        {
            accountRepo = new AccountRepository();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!IsInputEmpty())
            {
                string accountID = txtAccountID.Text;
                string password = txtPassword.Text;
                EmployeeAccountObject account;
                if ((account = accountRepo.Login(accountID, password)) != null)
                {
                    MessageBox.Show($"Welcome Employee: {account.FullName} ", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var mdiParent = (this.MdiParent as MainForm);
                    mdiParent.Account = account;
                    mdiParent.HideLoginMenu();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Account ID or Password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            else
            {
                MessageBox.Show("Please enter Account ID and Password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsInputEmpty()
        {
            return string.IsNullOrEmpty(txtAccountID.Text) &&
                   string.IsNullOrEmpty(txtPassword.Text);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
