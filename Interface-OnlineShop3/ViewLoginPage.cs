﻿using Interface_OnlineShop.Customers.Service;
using Interface_OnlineShop3.Customers.Models;
using System;

namespace Interface_OnlineShop3
{
    public class ViewLoginPage
    {
        private ICustomerCommandService _customerCommandService;
        private ICustomerQueryService _customerQueryService;
        private Customer _customer;

        public ViewLoginPage(ICustomerCommandService customerCommandService, ICustomerQueryService customerQueryService)
        {
            _customerCommandService = customerCommandService;
            _customerQueryService = customerQueryService;
        }

        public void LoginMeniu()
        {
            Console.WriteLine("Apasati tasta 1 pentru logare");
            Console.WriteLine("Apasati tasta 2 pentru a va inregistra");
            Console.WriteLine("Apasati tasta 3 pentru a reseta parola");
        }

        public void Play()
        {
            bool running = true;
            while (running)
            {
                LoginMeniu();
                string alegere = Console.ReadLine();

                switch (alegere)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        NewRegistration();
                        break;
                    case "3":
                        ResetareParola();
                        break;
                    default:
                        running = false;
                        break;
                }
            }
        }

        public void Login()
        {
            Console.WriteLine("Introduceti id-ul tau:");
            int idLogin = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti parola ta:");
            string parolaLogin = Console.ReadLine();

            Customer customer = _customerQueryService.FindCustomerById(idLogin);
            if (customer != null && customer.Password == parolaLogin)
            {
                Console.WriteLine("V-ati logat cu succes, " + customer.FullName + "!");
                _customer = customer;
                
            }
            else
            {
                Console.WriteLine("Datele sunt gresite sau nu sunteti inregistrat.");
            }
        }

        public Customer GetAuthenticatedCustomer()
        {
            return _customer;
        }

        public void NewRegistration()
        {
            Console.WriteLine("Introduceti email:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string newPassword = Console.ReadLine();

            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti adresa de facturare:");
            string billingAddress = Console.ReadLine();

            int newId = _customerQueryService.GetAllCustomers().Count + 1;
            Customer newCustomer = new Customer(newId, email, newPassword, fullName, billingAddress);

            Customer addedCustomer = _customerCommandService.AddCustomer(newCustomer);

            if (addedCustomer != null)
            {
                Console.WriteLine("Cont creat cu succes!");
            }
            else
            {
                Console.WriteLine("A aparut o eroare. Va rugam incercati din nou.");
            }
        }

        public void ResetareParola()
        {
            Console.WriteLine("Introduceti ID-ul tau:");
            int idWanted = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti noua parola:");
            string newPassword = Console.ReadLine();

            Customer customerToUpdate = _customerQueryService.FindCustomerById(idWanted);
            if (customerToUpdate != null)
            {
                customerToUpdate.Password = newPassword;
                _customerCommandService.UpdateCustomer(idWanted, customerToUpdate);
                Console.WriteLine("Parola a fost resetata cu succes.");
            }
            else
            {
                Console.WriteLine("Utilizatorul nu a fost gasit.");
            }
        }
    }
}
