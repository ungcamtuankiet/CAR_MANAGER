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
    public class CustomerDAO : BaseDAO
    {
        private CustomerDAO() : base() { }
        private static readonly object lockInstance = new object();
        private static CustomerDAO instance;
        public static CustomerDAO Instance
        {
            get
            {
                lock(lockInstance)
                {
                    return instance == null ? instance = new CustomerDAO() : instance;
                }
            }
        }

        public List<CustomerObject> GetCustomerList()
        {
            List<CustomerObject> list = new();
            try
            {
                using (connection)
                {
                    string query = 
                        " SELECT IdentityNumber, FullName, DateOfBirth, Gender, PhoneNumber, Address, Status " +
                        " FROM Customers " +
                        " WHERE Status = 1 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        while (reader.Read())
                        {
                            var customer = new CustomerObject()
                            {
                                IdentityNumber = reader.GetString(0),
                                FullName = reader.GetString(1),
                                DateOfBirth = reader.GetDateTime(2),
                                Gender = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Address = reader.GetString(5),
                                Status = reader.GetBoolean(6)
                            };
                            list.Add(customer);
                        }
                    }
                }
            } 
            catch (Exception)
            {

            }
            return list;
        }

        public CustomerObject GetCustomer(string customerID)
        {
            CustomerObject customer = null;
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT FullName, DateOfBirth, Gender, PhoneNumber, Address, Status " +
                        " FROM Customers " +
                        " WHERE IdentityNumber = @customerID AND Status = 1 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection, new SqlParameter("@customerID", customerID)))
                    {
                        while (reader.Read())
                        {
                            customer = new CustomerObject()
                            {
                                IdentityNumber = customerID,
                                FullName = reader.GetString(0),
                                DateOfBirth = reader.GetDateTime(1),
                                Gender = reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                Address = reader.GetString(4),
                                Status = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return customer;
        }

        public List<string> GetCustomerIDList()
        {
            List<string> list = new();
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT IdentityNumber " +
                        " FROM Customers " +
                        " WHERE Status = 1 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }

        public bool AddNewCustomer(CustomerObject customer)
        {
            bool valid = false;
            try
            {
                string query =
                    " INSERT INTO Customers (IdentityNumber, FullName, DateOfBirth, Gender, PhoneNumber, Address, Status) " +
                    " VALUES (@identityNumber, @fullName, @dateOfBirth, @gender, @phoneNumber, @address, 1) ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@identityNumber", customer.IdentityNumber),
                    new SqlParameter("@fullName", customer.FullName),
                    new SqlParameter("@dateOfBirth", customer.DateOfBirth),
                    new SqlParameter("@gender", customer.Gender),
                    new SqlParameter("@phoneNumber", customer.PhoneNumber),
                    new SqlParameter("@address", customer.Address)
                };
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, paramList);
            }
            catch (Exception)
            {

            }
            return valid;
        }

        public bool UpdateCustomer(CustomerObject customer)
        {
            bool valid = false;
            try
            {
                string query =
                    " UPDATE Customers " +
                    " SET FullName = @fullName, DateOfBirth = @dateOfBirth, Gender = @gender, PhoneNumber = @phoneNumber, Address = @address, Status = @status " +
                    " WHERE IdentityNumber = @identityNumber ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@identityNumber", customer.IdentityNumber),
                    new SqlParameter("@fullName", customer.FullName),
                    new SqlParameter("@dateOfBirth", customer.DateOfBirth),
                    new SqlParameter("@gender", customer.Gender),
                    new SqlParameter("@phoneNumber", customer.PhoneNumber),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@status", customer.Status)
                };
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, paramList);
            }
            catch (Exception)
            {

            }
            return valid;
        }

        public bool DeleteCustomer(string customerID)
        {
            bool valid = false;
            try
            {
                string query =
                    " UPDATE Customers " +
                    " SET Status = 0 " +
                    " WHERE IdentityNumber = @customerID ";
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, new SqlParameter("@customerID", customerID));
            }
            catch (Exception)
            {

            }
            return valid;
        }
    }
}
