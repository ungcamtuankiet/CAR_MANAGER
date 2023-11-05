using System.Collections.Generic;
using System.Linq;
using BusinessObject;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDAO instance = CustomerDAO.Instance;
        public IEnumerable<CustomerObject> GetCustomerList() => instance.GetCustomerList();
        public CustomerObject GetCustomer(string customerID) => instance.GetCustomer(customerID);
        public IEnumerable<string> GetCustomerIDList() => instance.GetCustomerIDList();
        public bool AddNewCustomer(CustomerObject customer) => instance.AddNewCustomer(customer);
        public bool UpdateCustomer(CustomerObject customer) => instance.UpdateCustomer(customer);
        public bool DeleteCustomer(string customerID) => instance.DeleteCustomer(customerID);
    }
}
