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
    public partial class CustomerInsertUpdateForm : Form
    {
        private ICustomerRepository customerRepo;
        private CustomerObject customerObject;
        public CustomerInsertUpdateForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                customerRepo = new CustomerRepository();
                InitializeComponent();
            }
        }
        public CustomerInsertUpdateForm(EmployeeAccountObject account, CustomerObject customerObject)
        {
            if (account != null)
            {
                this.customerObject = customerObject;
                customerRepo = new CustomerRepository();
                InitializeComponent();
            }
        }

        private void CustomerInsertUpdateForm_Load(object sender, EventArgs e)
        {
            cmbGender.SelectedIndex = 0;
            if (customerObject != null)
            {
                lblStatus.Visible = true;
                checkBoxStatus.Visible = true;
                checkBoxStatus.Checked = customerObject.Status;
                txtMaskedID.ReadOnly = true;
                txtMaskedID.Text = customerObject.IdentityNumber;
                txtFullName.Text = customerObject.FullName;
                dtpDOB.Value = customerObject.DateOfBirth;
                cmbGender.SelectedItem = customerObject.Gender;
                txtMaskedPhone.Text = customerObject.PhoneNumber;
                txtAddress.Text = customerObject.Address;
            }
        }

        private bool IsInputNull()
        {
            return string.IsNullOrWhiteSpace(txtMaskedID.Text) ||
                   string.IsNullOrEmpty(txtFullName.Text) ||
                   cmbGender.SelectedIndex < 0 ||
                   string.IsNullOrWhiteSpace(txtMaskedPhone.Text) ||
                   string.IsNullOrEmpty(txtAddress.Text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!IsInputNull())
            {
                if (IsInputValid())
                {
                    var customer = GetCustomerFromInput();
                    if (customerObject != null)
                    {
                        UpdateCustomer(customer);
                    }
                    else
                    {
                        InsertCustomer(customer);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CustomerObject GetCustomerFromInput()
        {
            var customer = new CustomerObject()
            {
                IdentityNumber = txtMaskedID.Text.Trim(),
                FullName = txtFullName.Text,
                DateOfBirth = dtpDOB.Value,
                Gender = cmbGender.Text,
                PhoneNumber = txtMaskedPhone.Text,
                Address = txtAddress.Text,
                Status = checkBoxStatus.Checked
            };
            return customer;
        }

        private bool IsInputValid()
        {
            bool valid = true;
            if (txtMaskedID.Text.Length >= 9)
            {
                if (txtMaskedPhone.Text.Length != 10)
                {
                    valid = false;
                    MessageBox.Show("Phone Number must have 10 numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                valid = false;
                MessageBox.Show("CustomerID must have 9-12 numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valid;
        }

        private void UpdateCustomer(CustomerObject customerUpdate)
        {
            bool valid = customerRepo.UpdateCustomer(customerUpdate);
            if (valid)
            {
                MessageBox.Show("Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertCustomer(CustomerObject customerInsert)
        {
            bool valid = customerRepo.AddNewCustomer(customerInsert);
            if (valid)
            {
                MessageBox.Show("Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to Insert", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
