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
    public partial class InvoiceCreateForm : Form
    {
        private EmployeeAccountObject account;
        private ICustomerRepository customerRepo;
        private ICarRepository carRepo;
        private IInvoiceRepository invoiceRepo;
        private Dictionary<int, InvoiceDetailObject> detailList;
        public InvoiceCreateForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                this.account = account;
                customerRepo = new CustomerRepository();
                carRepo = new CarRepository();
                invoiceRepo = new InvoiceRepository();
                detailList = new();
                InitializeComponent();
            }
        }

        private void CreateOrderForm_Load(object sender, EventArgs e)
        {
            GenerateGuid();
            lblEmployeeIDDisplay.Text = account.AccountID;
            txtDateTime.Text = DateTime.Now.ToString();
            LoadDetailList();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateGuid();
        }

        private void GenerateGuid()
        {
            txtInvoiceID.Text = Guid.NewGuid().ToString();
            txtDateTime.Text = DateTime.Now.ToString();
        }

        private void HideDeleteMenu()
        {
            if (gridDetail.Rows.Count > 0)
            {
                stripMenuRemove.Enabled = true;
            }
            else
            {
                stripMenuRemove.Enabled = false;
            }
        }

        private void LoadDetailList()
        {
            var car = carRepo.GetCarList();
            var list = (from detail in detailList.Values
                       join c in car on detail.CarId equals c.CarId
                       select new { c.CarId, c.CarModel, c.CarName, detail.Quantity, detail.CurrentPrice, Total = detail.Quantity*detail.CurrentPrice }).ToList();
            gridDetail.DataSource = null;
            gridDetail.DataSource = list;
            CountTotal();
            HideDeleteMenu();
        }

        private void CountTotal()
        {
            var detailValues = detailList.Values.ToList();
            var total = detailValues.Select(d => new { Total = d.CurrentPrice * d.Quantity })
                                    .Sum(d => d.Total);
            txtTotal.Text = total.ToString();             
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCarID.Text))
            {
                int carID = Convert.ToInt32(txtCarID.Text);
                var carObject = carRepo.GetCar(carID);
                if (carObject != null)
                {
                    var detail = new InvoiceDetailObject()
                    {
                        CarId = carObject.CarId,
                        Quantity = 1
                    };
                    InvoiceDetailInsertUpdateForm insertForm = new (detail);
                    if (insertForm.ShowDialog() == DialogResult.OK)
                    {
                        detail = insertForm.InvoiceDetail;
                        AddDetailToList(detail);
                        LoadDetailList();
                    }
                    txtCarID.Clear();
                }
                else
                {
                    MessageBox.Show("Car Id not Exist or Out of stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter Car Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (IsInputValid())
            {
                if (detailList.Count > 0)
                {
                    int carID = -1;
                    CarObject carObject;
                    foreach (var pair in detailList)
                    {
                        carObject = carRepo.GetCar(pair.Key);
                        if (!(carObject != null && pair.Value.Quantity <= carObject.TotalQuantity))
                        {
                            carID = pair.Key;
                            break;
                        }
                    }
                    if (carID < 0)
                    {
                        bool valid = invoiceRepo.AddInvoice(GetInvoiceObjectFromInput(), detailList);
                        if (valid)
                        {
                            MessageBox.Show("Successfully Created", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to Create Invoice", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else MessageBox.Show($"Car ID: {carID} is not exist or out of stock ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
                else
                {
                    var result = MessageBox.Show($"Invoice is empty - Cancel process ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stripMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stripMenuRemove_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Remove those cars from order ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in gridDetail.SelectedRows)
                {
                    int carID = Convert.ToInt32(row.Cells["CarId"].Value);
                    detailList.Remove(carID);                   
                }
                LoadDetailList();
            }
        }

        private void AddDetailToList(InvoiceDetailObject detail)
        {
            if (detail != null && detail.Quantity > 0)
            {
                int carID = detail.CarId;
                var carObject = carRepo.GetCar(carID);
                if (carObject != null)
                {
                    if (detailList.ContainsKey(carID))
                    {
                        var oldDetail = detailList[carID];
                        int totalQuantity = detail.Quantity + oldDetail.Quantity;
                        if (totalQuantity > carObject.TotalQuantity)
                        {
                            oldDetail.Quantity = carObject.TotalQuantity;
                            MessageBox.Show("Car stock is not enough - Quantity will remain", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            oldDetail.Quantity = totalQuantity;
                        }
                        oldDetail.CurrentPrice = carObject.Price;
                    }
                    else
                    {
                        detail.CurrentPrice = carObject.Price;
                        detailList.Add(carID, detail);
                    }
                }
                else
                {
                    detailList.Remove(carID);
                }
            }
        }

        private void gridDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1 && e.ColumnIndex > -1)
            {
                var detail = GetDetailObjectFromCell(row);
                InvoiceDetailInsertUpdateForm updateForm = new(detail);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    detail = updateForm.InvoiceDetail;
                    if (detail != null)
                    {
                        int carID = detail.CarId;
                        var carObject = carRepo.GetCar(carID);
                        if (carObject != null)
                        {
                            int newQuantity = detail.Quantity;
                            if ( newQuantity > 0)
                            {
                                var pos = detailList[carID];
                                pos.CurrentPrice = carObject.Price;
                                pos.Quantity = newQuantity > carObject.TotalQuantity ? carObject.TotalQuantity : newQuantity;
                            }
                            else
                            {
                                detailList.Remove(carID);
                            }
                        }
                        else
                        {
                            detailList.Remove(carID);
                        }
                    }
                    LoadDetailList();
                }
            }
        }

        private InvoiceDetailObject GetDetailObjectFromCell (int row)
        {
            var cells = gridDetail.Rows[row].Cells;
            var detail = new InvoiceDetailObject()
            {
                CarId = Convert.ToInt32(cells["CarId"].Value),
                Quantity = Convert.ToInt32(cells["Quantity"].Value)
            };
            return detail;
        }

        private InvoiceObject GetInvoiceObjectFromInput()
        {
            var invoice = new InvoiceObject()
            {
                InvoiceID = Guid.Parse(txtInvoiceID.Text),
                CustomerID = txtMaskedCustomerID.Text.Trim(),
                EmployeeID = account.AccountID,
                Total = Convert.ToDecimal(txtTotal.Text)
            };
            return invoice;
        }

        private bool IsInputValid()
        {
            bool valid = false;
            string customerID = txtMaskedCustomerID.Text.Trim();
            if ( !string.IsNullOrWhiteSpace(customerID) && customerID.Length >= 9)
            {
                if (customerRepo.GetCustomer(customerID) != null)
                {
                    valid = true;
                }
                else
                {
                    MessageBox.Show("Customer ID not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Customer ID must >= 9 digit numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valid;
        }
    }
}
