using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class InvoiceDAO : BaseDAO
    {
        private InvoiceDAO() : base() { }
        private static readonly object instanceLock = new object();
        private static InvoiceDAO instance;
        public static InvoiceDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    return instance == null ? instance = new InvoiceDAO() : instance;
                }
            }
        }

        public List<InvoiceObject> GetInvoiceList()
        {
            var list = new List<InvoiceObject>();
            using(connection)
            {
                string query =
                    " SELECT InvoiceID, CustomerID, EmployeeID, CreateDate, Total " +
                    " FROM Invoices ";
                using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                {
                    while (reader.Read())
                    {
                        var invoice = new InvoiceObject()
                        {
                            InvoiceID = reader.GetGuid(0),
                            CustomerID = reader.GetString(1),
                            EmployeeID = reader.GetString(2),
                            CreateDate = reader.GetDateTime(3),
                            Total = reader.GetDecimal(4)
                        };
                        list.Add(invoice);
                    }
                }
            }
            return list;
        }
        public List<InvoiceDetailObject> GetAllDetailList()
        {
            var list = new List<InvoiceDetailObject>();
            using (connection)
            {
                string query =
                    " SELECT InvoiceID, CarID, Quantity, CurrentPrice " +
                    " FROM InvoiceDetails ";
                using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                {
                    while (reader.Read())
                    {
                        var detail = new InvoiceDetailObject()
                        {
                            InvoiceID = reader.GetGuid(0),
                            CarId = reader.GetInt32(1),
                            Quantity = reader.GetInt32(2),
                            CurrentPrice = reader.GetDecimal(3),
                        };
                        list.Add(detail);
                    }
                }
            }
            return list;
        }

        public List<InvoiceDetailObject> GetInvoiceDetailList(Guid invoiceID)
        {
            var list = new List<InvoiceDetailObject>();
            using (connection)
            {
                string query =
                    " SELECT CarID, Quantity, CurrentPrice " +
                    " FROM InvoiceDetails " +
                    " WHERE InvoiceID = @invoiceID ";
                using (var reader = Provider.GetReader(query, CommandType.Text, out connection, new SqlParameter("@invoiceID", invoiceID)))
                {
                    while (reader.Read())
                    {
                        var detail = new InvoiceDetailObject()
                        {
                            InvoiceID = invoiceID,
                            CarId = reader.GetInt32(0),
                            Quantity = reader.GetInt32(1),
                            CurrentPrice = reader.GetDecimal(2),
                        };
                        list.Add(detail);
                    }
                }
            }
            return list;
        }
        public bool AddInvoice(InvoiceObject invoice, Dictionary<int, InvoiceDetailObject> detailList)
        {
            bool valid = false;
            try
            {
                string invoiceQ =
                    " INSERT INTO Invoices (InvoiceID, CustomerID, EmployeeID, CreateDate, Total) " +
                    " VALUES (@invoiceID, @customerID, @employeeID, GETDATE(), @total) ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@invoiceID", invoice.InvoiceID),
                    new SqlParameter("@customerID", invoice.CustomerID),
                    new SqlParameter("@employeeID", invoice.EmployeeID),
                    new SqlParameter("@total", invoice.Total)
                };
                valid = Provider.ExecuteNonQuery(invoiceQ, CommandType.Text, out connection, paramList);
                if (valid)
                {
                    valid = InsertDetail(detailList, invoice.InvoiceID);
                }
            } 
            catch (Exception)
            {

            }
            return valid;
        }

        private bool InsertDetail(Dictionary<int, InvoiceDetailObject> detailList, Guid invoiceID)
        {
            bool valid = false;
            using(connection = new SqlConnection(GetConnectionString()))
            {
                if (connection!=null)
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string insertQ =
                                " INSERT INTO InvoiceDetails (InvoiceID, CarID, Quantity, CurrentPrice)" +
                                " VALUES (@invoiceID, @carID, @quantity, @currentPrice) ; ";
                            string updateQ =
                                " UPDATE Cars " +
                                " SET TotalQuantity = TotalQuantity - @quantity " +
                                " WHERE CarID = @carID";
                            foreach (var pair in detailList)
                            {
                                var command = connection.CreateCommand();
                                command.CommandText = insertQ + updateQ;
                                command.Transaction = transaction;
                                var paramList = new SqlParameter[]
                                {
                                    new SqlParameter("@invoiceID", invoiceID),
                                    new SqlParameter("@carID", pair.Key),
                                    new SqlParameter("@quantity", pair.Value.Quantity),
                                    new SqlParameter("@currentPrice", pair.Value.CurrentPrice),
                                };
                                command.Parameters.AddRange(paramList);
                                valid = command.ExecuteNonQuery() > 0;
                            }
                            if (valid)
                            {
                                transaction.Commit();
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            return valid;
        }  
    }
}
