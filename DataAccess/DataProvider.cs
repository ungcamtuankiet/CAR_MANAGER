using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class DataProvider
    {
        private string connectionString;

        public DataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IDataReader GetReader(string query, CommandType commandType, out SqlConnection con, params SqlParameter[] paramList)
        {
            IDataReader reader = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                var command = con.CreateCommand();
                command.CommandText = query;
                command.CommandType = commandType;
                if (paramList != null)
                {
                    foreach (var param in paramList)
                    {
                        command.Parameters.Add(param);
                    }
                }
                reader = command.ExecuteReader();
            } catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            return reader;
        }

        public bool ExecuteNonQuery(string query, CommandType commandType, out SqlConnection con, params SqlParameter[] paramList)
        {
            bool valid = false;
            using (con = new SqlConnection(connectionString))
            {
                if (con != null)
                {
                    con.Open();
                    using var transaction = con.BeginTransaction();
                    try
                    {
                        var command = con.CreateCommand();
                        command.CommandText = query;
                        command.Transaction = transaction;
                        if (paramList != null)
                        {
                            foreach (var param in paramList)
                            {
                                command.Parameters.Add(param);
                            }
                        }
                        valid = command.ExecuteNonQuery() > 0;
                        if (valid) transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        throw new Exception(err.Message);
                    }
                }
            }
            return valid;
        }
    }
}
