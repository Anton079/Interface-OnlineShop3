using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Customers.Service;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Service;
using System;

namespace Interface_OnlineShop3.playground
{
    public class ViewLoginPage
    {
        private Customer _customer;
        private IOrdersQueryService _ordersQueryService;
        private IOrdersCommandService _ordersCommandService;

        private IOrderDetailsQueryService _orderDetailsQueryService;
        private IOrderDetailsCommandService _orderDetailsCommandService;

        private ICustomerQueryService _customerQueryService;
        private ICustomerCommandService _customerCommandService;

        private IProductQueryService _productQueryService;
        private IProductComandService _productComandService;

        public ViewLoginPage(IOrdersQueryService ordersQueryService, IOrdersCommandService ordersCommandService,
                    IOrderDetailsQueryService orderDetailsQueryService, IOrderDetailsCommandService orderDetailsCommandService,
                    ICustomerQueryService customerQueryService, ICustomerCommandService customerCommandService,
                    IProductQueryService productQueryService, IProductComandService productComandService)
        {
            _ordersQueryService = ordersQueryService;
            _ordersCommandService = ordersCommandService;

            _orderDetailsQueryService = orderDetailsQueryService;
            _orderDetailsCommandService = orderDetailsCommandService;

            _customerQueryService = customerQueryService;
            _customerCommandService = customerCommandService;

            _productQueryService = productQueryService;
            _productComandService = productComandService;
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
                ViewUser viewUser = new ViewUser(_customer, _ordersCommandService, _orderDetailsCommandService, _productQueryService, _ordersQueryService, _orderDetailsQueryService);
                viewUser.Play();

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

            string userName = "userName";

            Console.WriteLine("Introduceti parola:");
            string newPassword = Console.ReadLine();

            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti adresa de facturare:");
            string billingAddress = Console.ReadLine();

            int newId = _customerQueryService.GetAllCustomers().Count + 1;
            Customer newCustomer = new Customer(newId, userName, email, newPassword, fullName, billingAddress);

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
