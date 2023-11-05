using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerObject> GetCustomerList();
        CustomerObject GetCustomer(string customerID);
        IEnumerable<string> GetCustomerIDList();
        bool AddNewCustomer(CustomerObject customer);
        bool UpdateCustomer(CustomerObject customer);
        bool DeleteCustomer(string customerID);
    }
}
