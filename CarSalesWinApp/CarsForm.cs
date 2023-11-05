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
using BusinessObject;

namespace CarSalesWinApp
{
    public partial class CarsForm : Form
    {
        private EmployeeAccountObject account;
        private ICarRepository carRepo;
        private IEnumerable<CarObject> carList;
        public CarsForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                this.account = account;
                carRepo = new CarRepository();
                InitializeComponent();
            }
            
        }

        private void CarsForm_Load(object sender, EventArgs e)
        {
            ReloadCarList();
        }

        private void LoadCarList(IEnumerable<CarObject> list)
        {
            gridViewCar.DataSource = null;
            gridViewCar.DataSource = list;
            HideDeleteCar();
        }

        public void ReloadCarList()
        {
            carList = carRepo.GetCarList();
            LoadCarList(carList);
        }
        private void HideDeleteCar()
        {
            if (gridViewCar.RowCount > 0)
            {
                stripMenuDeleteCar.Enabled = true;
            }
            else
            {
                stripMenuDeleteCar.Enabled = false;
            }
        }

        private void stripMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridViewCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1 && e.ColumnIndex > -1)
            {
                var carObject = GetCarObjectFromCell(row);
                CarInsertUpdateForm updateForm = new CarInsertUpdateForm(account, carObject);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadCarList();
                }
            }
        }

        private void stripMenuNewCar_Click(object sender, EventArgs e)
        {
            CarInsertUpdateForm insertForm = new CarInsertUpdateForm(account);
            if (insertForm.ShowDialog() == DialogResult.OK)
            {
                ReloadCarList();
            }
        }

        private CarObject GetCarObjectFromCell(int row)
        {
            var cell = gridViewCar.Rows[row].Cells;
            var carObject = new CarObject()
            {
                CarId = (int)cell["CarId"].Value,
                CarModel = cell["CarModel"].Value.ToString(),
                CarName = cell["CarName"].Value.ToString(),
                Price = Convert.ToDecimal(cell["Price"].Value),
                LoadCapacity = (int)cell["LoadCapacity"].Value,
                SeatNum = (int)cell["SeatNum"].Value,
                FuelCapacity = (int)cell["FuelCapacity"].Value,
                CarType = (int)cell["CarType"].Value,
                TypeName = cell["TypeName"].Value.ToString(),
                Manufacturer = (int)cell["Manufacturer"].Value,
                ManuName = cell["ManuName"].Value.ToString(),
                TotalQuantity = (int)cell["TotalQuantity"].Value,
                Status = (bool) cell["Status"].Value
            };
            return carObject;
        }

        private void stripMenuDeleteCar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deactivate this car ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in gridViewCar.SelectedRows)
                {
                    int carID = (int)row.Cells["CarID"].Value;
                    carRepo.DeleteCar(carID);
                }
                ReloadCarList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            int stock = cmbStock.SelectedIndex >= 0 ? cmbStock.SelectedIndex : 2;
            Search(search, stock);
        }

        private void Search(string search = "", int stock = 2)
        {
            var list = (from car in carList
                       where car.CarModel.ToLower().Contains(search) || 
                             car.CarName.ToLower().Contains(search) ||
                             car.CarId.ToString().Equals(search) || 
                             car.ManuName.ToLower().Contains(search) ||
                             car.TypeName.ToLower().Contains(search)
                       select car).ToList();
            if (stock == 0) list = list.Where(car => car.TotalQuantity == 0).ToList();
            else if (stock == 1) list = list.Where(car => car.TotalQuantity > 0).ToList();
            LoadCarList(list);
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ReloadCarList();
            cmbStock.SelectedIndex = 2;
        }

        private void CarsForm_Activated(object sender, EventArgs e)
        {
            ReloadCarList();
        }
    }
}
