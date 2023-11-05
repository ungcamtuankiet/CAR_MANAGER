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
    public partial class StatisticForm : Form
    {
        private IInvoiceRepository invoiceRepo;
        private ICarRepository carRepo;
        public StatisticForm(EmployeeAccountObject account)
        {
            if (account != null)
            {
                invoiceRepo = new InvoiceRepository();
                carRepo = new CarRepository();
                InitializeComponent();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateStatistic();
        }

        private void CreateStatistic()
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;
            if (fromDate.CompareTo(toDate) > 0)
            {
                var result = MessageBox.Show($"Do you mean to create report in time interval: \nFrom {toDate}\nTo {fromDate.AddHours(23).AddMinutes(59).AddSeconds(59)} ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    fromDate = toDate;
                    toDate = dtpFrom.Value;
                    dtpFrom.Value = fromDate;
                    dtpTo.Value = toDate;
                }
                else return;
            }
            toDate = toDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            var invoiceList = invoiceRepo.GetInvoiceList();
            if (radioCar.Checked)
            {
                var detailList = invoiceRepo.GetAllDetailList();
                var carList = carRepo.GetCarList();
                var list = (from d in detailList
                            join c in carList on d.CarId equals c.CarId
                            join i in invoiceList on d.InvoiceID equals i.InvoiceID
                            where i.CreateDate.Date.CompareTo(fromDate) >= 0 && i.CreateDate.Date.CompareTo(toDate) <= 0
                            group d by new { c.CarId, c.CarModel, c.CarName, d.CurrentPrice })
                            .Select(d => new { d.Key.CarId, d.Key.CarModel, d.Key.CarName, TotalSold = d.Sum(q => q.Quantity), d.Key.CurrentPrice})
                            .OrderBy(d => d.CarId)
                            .ToList();
                LoadStatistic(list);
            }
            else
            {
                var list = invoiceList.GroupBy(d => d.CreateDate.Date)
                    .Select(d => new { Date = d.Key, Total = d.Sum(t => t.Total)})
                    .Where( d => d.Date.CompareTo(fromDate) >= 0 && d.Date.CompareTo(toDate) <= 0 )
                    .ToList();
                LoadStatistic(list);
            }            
        }

        private void LoadStatistic<T> (List<T> list)
        {
            gridStatistic.DataSource = null;
            gridStatistic.DataSource = list;
        }

        private void StatisticForm_Load(object sender, EventArgs e)
        {
            CreateStatistic();
        }
    }
}
