using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        private EmployeeAccountDAO instance = EmployeeAccountDAO.Instance;
        public EmployeeAccountObject Login(string accountID, string password) => instance.Login(accountID, password);
    }
}
