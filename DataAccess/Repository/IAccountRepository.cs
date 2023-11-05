using System;
using BusinessObject;
namespace DataAccess
{
    public interface IAccountRepository
    {
        EmployeeAccountObject Login(string accountID, string password);
    }
}
