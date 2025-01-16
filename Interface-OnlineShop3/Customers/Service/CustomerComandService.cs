using Interface_OnlineShop3.Customers.Exceptions;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Customers.Repository;
using Interface_OnlineShop3.Orders.Exceptions;
using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Customers.Service
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
            if (_customerRepository.AddCustomer(customer) != null)
            {
                return customer;
            }
            else
            {
                throw new NullCustomerException();
            }
        }

        public int RemoveCustomer(int id)
        {
            if(_customerRepository.Remove(id)!= null)
            { 
                return id;
            }
            else
            {
                throw new CustomerNotFoundException();
            }
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            if (id == null)
            {
                throw new OrderNotFoundException();
            }

            _customerRepository.UpdateCustomer(id, customer);
            return customer;
        }


    }
}
