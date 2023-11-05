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
    public partial class ManufacturersForm : Form
    {
        private EmployeeAccountObject account;
        private ICarRepository carRepo;
        public ManufacturersForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                this.account = account;
                carRepo = new CarRepository();
                InitializeComponent();
            }
            
        }

        private void stripMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManufacturersForm_Load(object sender, EventArgs e)
        {
            ReloadManuList();
        }

        private void LoadManufacturerList(IEnumerable<ManufacturerObject> list)
        {
            gridManu.DataSource = null;
            gridManu.DataSource = list;
            HideDeleteMenu();
        }

        public void ReloadManuList()
        {
            var list = carRepo.GetManufacturerList();
            LoadManufacturerList(list);
        }

        private ManufacturerObject GetManufacturerFromCell(int row)
        {
            var cell = gridManu.Rows[row].Cells;
            var manu = new ManufacturerObject()
            {
                ManuID = (int) cell["ManuID"].Value,
                ManuName = cell["ManuName"].Value.ToString(),
                Status = (bool) cell["Status"].Value,
            };
            return manu;
        }

        private void gridManu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1 && e.ColumnIndex > -1)
            {
                var manu = GetManufacturerFromCell(row);
                ManufacturerInsertUpdateForm updateForm = new ManufacturerInsertUpdateForm(account, manu);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadManuList();
                }
            }
        }

        private void stripMenuNew_Click(object sender, EventArgs e)
        {
            ManufacturerInsertUpdateForm insertForm = new ManufacturerInsertUpdateForm(account);
            if (insertForm.ShowDialog() == DialogResult.OK)
            {
                ReloadManuList();
            }
        }

        private void HideDeleteMenu()
        {
            if (gridManu.RowCount > 0)
            {
                stripMenuDelete.Enabled = true;
            }
            else
            {
                stripMenuDelete.Enabled = false;
            }
        }
        private void stripMenuDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Delete those manufacturers - This also remove their Cars?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in gridManu.SelectedRows )
                {
                    int manuID = (int)row.Cells["ManuID"].Value;
                    carRepo.DeleteManufacturer(manuID);
                }
                ReloadManuList();
            }
        }

        private void ManufacturersForm_Activated(object sender, EventArgs e)
        {
            ReloadManuList();
        }
    }
}
