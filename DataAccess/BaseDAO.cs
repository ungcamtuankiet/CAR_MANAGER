using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseDAO
    {
        public DataProvider Provider { set; get; }
        protected SqlConnection connection = null;
        public BaseDAO() 
        {
            Provider = new DataProvider(GetConnectionString());
        }
        protected string GetConnectionString()
        {
            var conStringBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())                                                            
                                    .AddJsonFile("appsettings.json", true, true);
            return conStringBuilder.Build()["ConnectionString:DBConString"];
        }
    }
}
