using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Customers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Customer> customerList;

        public CustomerRepository()
        {
            customerList = new List<Customer>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GetFilePath()))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        Customer customer = new Customer(line);
                        this.customerList.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folder = Path.Combine(currentDirectory, "data");
            string file = Path.Combine(folder, "Customer");
            return file;
        }

        private void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetFilePath()))
                {
                    sw.WriteLine(ToSaveAll());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string ToSaveAll()
        {
            string save = "";
            for (int i = 0; i < customerList.Count; i++)
            {
                save += customerList[i].ToString();
                if (i < customerList.Count - 1)
                {
                    save += "\n";
                }
            }
            return save;
        }

        // CRUD 

        public List<Customer> GetAll()
        {
            return customerList;
        }

        public Customer AddCustomer(Customer customer)
        {
            this.customerList.Add(customer);
            this.SaveData();
            return customer;
        }

        public Customer Remove(int id)
        {
            Customer cs = FindById(id);

            customerList.Remove(cs);
            this.SaveData();
            return cs;
        }

        public Customer FindById(int id)
        {
            foreach (Customer customer in customerList)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            Customer customerUpdate = FindById(id);

            if (customerUpdate != null)
            {

                customerUpdate.FullName = customer.FullName;
                customerUpdate.Password = customer.Password;
                customerUpdate.FullName = customer.FullName;
                customerUpdate.BillingAddress = customer.BillingAddress;

                this.SaveData();
            }
            Console.WriteLine("Este null");
            return customerUpdate;
        }
    }
}
