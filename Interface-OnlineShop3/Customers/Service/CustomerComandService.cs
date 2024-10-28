using Interface_OnlineShop.Customers.Repository;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Customers.Service
{
    public class CustomerCommandService : ICustomerCommandService
    {
        private ICustomerRepository _customerRepository;

        public CustomerCommandService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                _customerRepository.AddCustomer(customer);
                return customer;
            }
            return null;
        }

        public int RemoveCustomer(int id)
        {
            if (id != null)
            {
                _customerRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            if(id != -1 &&  customer != null)
            {
                _customerRepository.UpdateCustomer(id,customer);
                return customer;
            }
            return null;
        }


    }
}
