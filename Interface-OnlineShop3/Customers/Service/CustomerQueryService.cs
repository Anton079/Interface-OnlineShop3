using Interface_OnlineShop.Customers.Repository;
using Interface_OnlineShop3.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Customers.Service
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
            return _customerRepository.FindById(id);
        }
    }
}
