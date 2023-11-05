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
    public partial class InvoiceDetailInsertUpdateForm : Form
    {
        private ICarRepository carRepo;
        public InvoiceDetailObject InvoiceDetail { set; get; }
        private int maxQuantity;
        public InvoiceDetailInsertUpdateForm(InvoiceDetailObject detail)
        {
            carRepo = new CarRepository();
            InvoiceDetail = detail;
            InitializeComponent();
        }

        private void InvoiceDetailInsertUpdateForm_Load(object sender, EventArgs e)
        {
            numQuantity.Value = InvoiceDetail.Quantity;
            numQuantity.Maximum = int.MaxValue;
            var carObject = carRepo.GetCar(InvoiceDetail.CarId);
            if (carObject != null)
            {
                maxQuantity = carObject.TotalQuantity;
            }
            else
            {
                numQuantity.Value = 0;
                maxQuantity = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(numQuantity.Value);
            if (quantity > maxQuantity) MessageBox.Show($"Our current stock of CarId: {InvoiceDetail.CarId} is {maxQuantity} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                InvoiceDetail.Quantity = quantity;
                this.DialogResult = DialogResult.OK;
            }
        }

    }
}
