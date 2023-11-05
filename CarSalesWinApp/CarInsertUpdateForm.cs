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
    public partial class CarInsertUpdateForm : Form
    {
        ICarRepository carRepo;
        CarObject carObject;
        public CarInsertUpdateForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                carRepo = new CarRepository();
                InitializeComponent();
            } 
        }
        public CarInsertUpdateForm(EmployeeAccountObject account, CarObject car)
        {
            if (account != null)
            {
                carRepo = new CarRepository();
                carObject = car;
                InitializeComponent();
            }
        }

        private void CarInsertUpdateForm_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            numPrice.Maximum = decimal.MaxValue;
            numLoadCapacity.Maximum = int.MaxValue;
            numSeatNumber.Maximum = int.MaxValue;
            numFuelCapacity.Maximum = int.MaxValue;
            numTotalQuantity.Maximum = int.MaxValue;
            if (carObject != null)
            {
                txtCarModel.Text = carObject.CarModel;
                txtCarName.Text = carObject.CarName;
                numPrice.Value = carObject.Price;
                numLoadCapacity.Value = carObject.LoadCapacity;
                numSeatNumber.Value = carObject.SeatNum;
                numFuelCapacity.Value = carObject.FuelCapacity;
                cmbCarTypeName.SelectedItem = carObject.TypeName;
                cmbCarTypeID.SelectedIndex = cmbCarTypeName.SelectedIndex;
                cmbManuName.SelectedItem = carObject.ManuName;
                cmbManuID.SelectedIndex = cmbManuName.SelectedIndex;
                numTotalQuantity.Value = carObject.TotalQuantity;
                lblStatus.Visible = true;
                checkBoxStatus.Visible = true;
                checkBoxStatus.Checked = carObject.Status;
            }
        }

        private void LoadComboBox()
        {
            var manufacturerList = carRepo.GetManufacturerList();
            var carTypeList = carRepo.GetCarTypeList();
            foreach (var manu in manufacturerList)
            {
                cmbManuID.Items.Add(manu.ManuID);
                cmbManuName.Items.Add(manu.ManuName);
            }
            foreach (var type in carTypeList)
            {
                cmbCarTypeName.Items.Add(type.TypeName);
                cmbCarTypeID.Items.Add(type.TypeID);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!IsInputNull())
            {
                var carInserUpdateObject = GetCarObjectFromInput();
                if (carObject != null)
                {
                    UpdateCar(carInserUpdateObject);
                }
                else
                {
                    InsertCar(carInserUpdateObject);
                }               
            }
            else
            {
                MessageBox.Show("Please fill all the blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCar(CarObject carUpdate)
        {
            carUpdate.CarId = carObject.CarId;
            bool valid = carRepo.UpdateCar(carUpdate);
            if (valid)
            {
                MessageBox.Show("Update Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to update car", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertCar(CarObject carInsert)
        {
            bool valid = carRepo.AddNewCar(carInsert);
            if (valid)
            {
                MessageBox.Show("Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to add new car", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsInputNull()
        {
            return string.IsNullOrEmpty(txtCarModel.Text) ||
                   string.IsNullOrEmpty(txtCarName.Text) ||
                   cmbCarTypeName.SelectedIndex < 0 ||
                   cmbManuName.SelectedIndex < 0;
        }

        private CarObject GetCarObjectFromInput()
        {
            cmbCarTypeID.SelectedIndex = cmbCarTypeName.SelectedIndex;
            cmbManuID.SelectedIndex = cmbManuName.SelectedIndex;
            var carObject = new CarObject()
            {
                CarModel = txtCarModel.Text,
                CarName = txtCarName.Text,
                Price = numPrice.Value,
                LoadCapacity = Convert.ToInt32(numLoadCapacity.Value),
                SeatNum = Convert.ToInt32(numSeatNumber.Value),
                FuelCapacity = Convert.ToInt32(numFuelCapacity.Value),
                Manufacturer = (int)cmbManuID.SelectedItem,
                CarType = (int)cmbCarTypeID.SelectedItem,
                TotalQuantity = Convert.ToInt32(numTotalQuantity.Value),
                Status = checkBoxStatus.Checked
            };
            return carObject;
        }
    }
}
