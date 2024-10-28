using Interface_OnlineShop3.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Customers.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();

        Customer AddCustomer(Customer customer);

        Customer Remove(int id);

        Customer FindById(int id);

        Customer UpdateCustomer(int id,Customer customer);

    }
}
