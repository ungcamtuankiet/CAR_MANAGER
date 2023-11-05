using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;

namespace CarSalesWinApp
{
    public partial class ManufacturerInsertUpdateForm : Form
    {
        private ICarRepository carRepo;
        private ManufacturerObject manuObject;
        public ManufacturerInsertUpdateForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                carRepo = new CarRepository();
                InitializeComponent();
            }
        } 
        public ManufacturerInsertUpdateForm(EmployeeAccountObject account, ManufacturerObject manuObject)
        {
            if (account != null)
            {
                carRepo = new CarRepository();
                this.manuObject = manuObject;
                InitializeComponent();
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!IsInputNull())
            {
                var manu = GetManufacturerFromInput();
                if (manuObject != null)
                {
                    manu.ManuID = manuObject.ManuID;
                    UpdateManufacturer(manu);
                }
                else
                {
                    InsertManufacturer(manu);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the blanks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManufacturerInsertUpdateForm_Load(object sender, EventArgs e)
        {
            if (manuObject != null)
            {
                txtManuName.Text = manuObject.ManuName;
                checkBoxStatus.Checked = manuObject.Status;
                checkBoxStatus.Visible = true;
                lblStatus.Visible = true;
            }
        }

        private bool IsInputNull()
        {
            return string.IsNullOrEmpty(txtManuName.Text);
        }

        private ManufacturerObject GetManufacturerFromInput()
        {
            var manu = new ManufacturerObject()
            {
                ManuName = txtManuName.Text,
                Status = checkBoxStatus.Checked    
            };
            return manu;
        }

        private void UpdateManufacturer(ManufacturerObject updateObject)
        {
            bool valid = carRepo.UpdateManufacturer(updateObject);
            if (valid)
            {
                MessageBox.Show("Successfully Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertManufacturer(ManufacturerObject insertObject)
        {
            bool valid = carRepo.AddManufacturer(insertObject);
            if (valid)
            {
                MessageBox.Show("Successfully Added", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
