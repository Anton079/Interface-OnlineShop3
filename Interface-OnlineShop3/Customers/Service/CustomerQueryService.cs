using Interface_OnlineShop3.Customers.Exceptions;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Customers.Repository;
using Interface_OnlineShop3.Orders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Customers.Service
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private ICustomerRepository _customerRepository;

        public CustomerQueryService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer FindCustomerById(int id)
        {
            Customer customer = _customerRepository.FindById(id); 

            if (customer != null)
            {
                return customer;
            }
            else
            {
                throw new CustomerNotFoundException();
            }
        }

    }
}
