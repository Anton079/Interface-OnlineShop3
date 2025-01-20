using Interface_OnlineShop3.Admins.Extensions;
using Interface_OnlineShop3.Admins.Models;
using Interface_OnlineShop3.Admins.Repository;
using Interface_OnlineShop3.Admins.Service;
using Interface_OnlineShop3.Customers.Exceptions;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Customers.Repository;
using Interface_OnlineShop3.Customers.Service;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.Users.Models;
using Interface_OnlineShop3.Users.Service;
using System;

namespace Interface_OnlineShop3
{
    public class ViewLogin
    {
        private IAdminQueryService _adminQueryService;
        private IAdminRepository _adminRepository;

        private ICustomerQueryService _customerQueryService;
        private ICustomerRepository _customerRepository;

        private IOrdersCommandService _ordersCommandService;
        private IOrderDetailsCommandService _orderDetailsCommandService;

        private IProductQueryService _productQueryService;
        private IProductRepository _productRepository;
        private IProductComandService _productComandService;

        private IOrdersQueryService _ordersQueryService;
        private IOrderDetailsQueryService _orderDetailsQueryService;
        private ICos _cos;

        public ViewLogin(IProductComandService productComandService, IProductRepository productRepository, IAdminQueryService adminQueryService, IAdminRepository adminRepository,
       ICustomerQueryService customerQueryService, ICustomerRepository customerRepository,
       IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService,
       IProductQueryService productQueryService, IOrdersQueryService ordersQueryService,
       IOrderDetailsQueryService orderDetailsQueryService, ICos cos)
        {
            _productComandService = productComandService;
            _productRepository = productRepository;
            _adminQueryService = adminQueryService;
            _adminRepository = adminRepository;
            _customerQueryService = customerQueryService;
            _customerRepository = customerRepository;
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
            _cos = cos;
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
                        Console.WriteLine("Optiune invalida. Reincercati.");
                        break;
                }
            }
        }

        public void Login()
        {
            Console.WriteLine("Introduceti idul:");
            int idLogin = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti parola:");
            string parolaLogin = Console.ReadLine();

            try
            {
                Admin adminWanted = _adminQueryService.FindAdminById(idLogin);

                Customer customerWanted = _customerQueryService.FindCustomerById(idLogin);

                if (adminWanted != null && adminWanted.Password == parolaLogin)
                {
                    ViewAdmin adminView = new ViewAdmin(
                        adminWanted,
                        _productRepository,
                        _productComandService,
                        _ordersCommandService,
                        _orderDetailsCommandService,
                        _productQueryService,
                        _ordersQueryService,
                        _orderDetailsQueryService
                    );
                    Console.WriteLine($"Bine ati venit, Admin: {adminWanted.FullName}");
                    adminView.Play();
                }
                else if (customerWanted != null && customerWanted.Password == parolaLogin)
                {
                    MainView viewMain = new MainView(
                        customerWanted,
                        _cos,
                        _ordersCommandService,
                        _orderDetailsCommandService,
                        _productQueryService,
                        _ordersQueryService,
                        _orderDetailsQueryService
                    );
                    Console.WriteLine($"Bine ati venit, Client: {customerWanted.FullName}");
                    viewMain.Play();
                }
                else
                {
                    Console.WriteLine("ID sau parola gresite. Reincercati.");
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void NewRegistration()
        {
            Console.WriteLine("Selectati tipul de utilizator pentru inregistrare: Admin, Client, Utilizator");
            string tip = Console.ReadLine().ToLower();

            switch (tip)
            {
                case "admin":
                    RegisterAdmin();
                    break;
                case "client":
                    RegisterCustomer();
                    break;
                default:
                    Console.WriteLine("Tip utilizator invalid.");
                    break;
            }
        }

        private void RegisterAdmin()
        {
            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti numele de utilizator:");
            string username = Console.ReadLine();

            Console.WriteLine("Introduceti emailul:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string password = Console.ReadLine();

            try
            {
                int id = _adminRepository.GenerateId();
                Admin admin = new Admin(id, username, fullName, email, password, "Adresa Facturare");
                _adminRepository.AddAdmin(admin);
                Console.WriteLine($"Admin inregistrat cu succes! ID-ul dumneavoastra este: {id}");
            }
            catch (NullAdminException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RegisterCustomer()
        {
            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti numele de utilizator:");
            string username = Console.ReadLine();

            Console.WriteLine("Introduceti emailul:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string password = Console.ReadLine();

            try
            {
                int id = _customerRepository.GenerateId();
                Customer customer = new Customer(id, username, fullName, email, password, "Adresa Facturare");
                _customerRepository.AddCustomer(customer);
                Console.WriteLine($"Client inregistrat cu succes! ID-ul dumneavoastra este: {id}");
            }
            catch (NullCustomerException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ResetareParola()
        {
            Console.WriteLine("Introduceti tipul de utilizator (Admin, Client):");
            string tip = Console.ReadLine().ToLower();

            Console.WriteLine("Introduceti ID-ul:");
            int id = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti noua parola:");
            string parolaNoua = Console.ReadLine();

            switch (tip)
            {
                case "admin":
                    try
                    {
                        Admin admin = _adminQueryService.FindAdminById(id);

                        if (admin != null)
                        {
                            admin.Password = parolaNoua;
                            _adminRepository.UpdateAdmin(id, admin);
                            Console.WriteLine("Parola actualizata cu succes pentru Admin.");
                        }
                        else
                        {
                            Console.WriteLine("Adminul nu a fost gasit.");
                        }
                    }
                    catch (AdminNotFoundException ex) { Console.WriteLine(ex.Message); }
                    catch (NullAdminException ex ) { Console.WriteLine(ex.Message); }
                    break;

                case "client":
                    try
                    {
                        Customer customer = _customerQueryService.FindCustomerById(id);
                        if (customer != null)
                        {
                            customer.Password = parolaNoua;
                            _customerRepository.UpdateCustomer(id, customer);
                            Console.WriteLine("Parola actualizata cu succes pentru Client.");
                        }
                        else
                        {
                            Console.WriteLine("Clientul nu a fost gasit.");
                        }
                    }catch(CustomerNotFoundException ex) { Console.WriteLine(ex.Message); }
                    catch(NullCustomerException ex) { Console.WriteLine(ex.Message) ; }
                    break;

                default:
                    Console.WriteLine("Tip utilizator invalid.");
                    break;
            }
        }



    }
}
