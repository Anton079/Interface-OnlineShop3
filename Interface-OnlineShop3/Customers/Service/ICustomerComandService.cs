using Interface_OnlineShop3.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Customers.Service
{
    public interface ICustomerCommandService
    {
        Customer AddCustomer(Customer customer);

        int RemoveCustomer(int id);

        Customer UpdateCustomer(int id, Customer customer);

    }
}
