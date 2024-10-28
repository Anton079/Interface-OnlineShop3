using Interface_OnlineShop3.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Customers.Service
{
    public interface ICustomerQueryService
    {
        List<Customer> GetAllCustomers();

        Customer FindCustomerById(int id);

    }
}
