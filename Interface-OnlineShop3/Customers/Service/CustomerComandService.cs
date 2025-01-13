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
            try
            {
                _customerRepository.AddCustomer(customer);
                return customer;
            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int RemoveCustomer(int id)
        {
            try
            {
                _customerRepository.Remove(id);
                return id;
            }catch(OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            try
            {
                if (id == null)
                {
                    throw new OrderNotFoundException();
                }

                _customerRepository.UpdateCustomer(id, customer);
                return customer;

            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}
