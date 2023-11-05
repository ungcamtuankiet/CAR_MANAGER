using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class EmployeeAccountDAO : BaseDAO
    {
        private EmployeeAccountDAO() : base() { }
        private static readonly object instanceLock = new object();
        private static EmployeeAccountDAO instance;
        public static EmployeeAccountDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    return instance == null ? instance = new EmployeeAccountDAO() : instance;
                }
            }
        }

        public EmployeeAccountObject Login(string accountID, string password)
        {
            EmployeeAccountObject emp = null;
            try
            {
                using (connection)
                {
                    string query = 
                        " SELECT FullName, IdentityNumber " +
                        " FROM Accounts" +
                        " WHERE AccountID = @accountID AND Password = @password AND Active = 1 ";
                    var paramList = new SqlParameter[]
                    {
                        new SqlParameter("@accountID", accountID),
                        new SqlParameter("@password", password)
                    };
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection, paramList))
                    {
                        if (reader.Read())
                        {
                            emp = new EmployeeAccountObject()
                            {
                                AccountID = accountID,
                                FullName = reader.GetString(0),
                                IdentityNumber = reader.GetString(1)
                            };
                        }
                    }
                }
            } 
            catch (Exception)
            {

            }
            return emp;
        }
    }
}
