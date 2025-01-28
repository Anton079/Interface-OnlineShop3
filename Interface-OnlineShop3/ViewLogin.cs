using Interface_OnlineShop3.Users.Repository;
using Interface_OnlineShop3.Users.Service;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.Users.Exceptions;
using Interface_OnlineShop3.Users.Models;
using System;
using Interface_OnlineShop3.Users.Service.Interface_OnlineShop3.Users.Service;

namespace Interface_OnlineShop3
{
    public class ViewLogin
    {
        private IOrdersCommandService _ordersCommandService;
        private IOrderDetailsCommandService _orderDetailsCommandService;

        private IProductQueryService _productQueryService;
        private IProductRepository _productRepository;
        private IProductComandService _productComandService;

        private IUserRepository _userRepository;
        private IUserQueryService _userQueryService;
        private IUserCommandService _userCommandService;

        private IOrdersQueryService _ordersQueryService;
        private IOrderDetailsQueryService _orderDetailsQueryService;
        private ICos _cos;

        public ViewLogin(IUserRepository userRepository, IUserQueryService userQueryService, IUserCommandService userCommandService, IProductComandService productComandService, IProductRepository productRepository,
       IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService,
       IProductQueryService productQueryService, IOrdersQueryService ordersQueryService,
       IOrderDetailsQueryService orderDetailsQueryService, ICos cos)
        {
            _userRepository = userRepository;
            _userCommandService = userCommandService;
            _productQueryService = productQueryService;

            _productComandService = productComandService;
            _productRepository = productRepository;
            _productQueryService = productQueryService;

            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;

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
                User user = _userQueryService.FindUserById(idLogin);

                switch (user.Type)
                {
                    case "Admin":
                        if (user is Admin admin)
                        {
                            ViewAdmin viewAdmin = new ViewAdmin(admin,
                                 _productRepository,
                                 _productComandService,
                                 _ordersCommandService,
                                 _orderDetailsCommandService,
                                 _productQueryService,
                                 _ordersQueryService,
                                 _orderDetailsQueryService);
                        }
                        break;

                    case "Customer":
                        if (user is Customer customer)
                        {
                            MainView viewCustomer = new MainView(customer, _cos,
                                _ordersCommandService,
                                _orderDetailsCommandService,
                                _productQueryService,
                                _ordersQueryService,
                                _orderDetailsQueryService);
                        }
                        break;

                    default:
                        Console.WriteLine("Tip utilizator necunoscut.");
                        break;
                }
            }
            catch (CustomerNotFoundException ex){ Console.WriteLine(ex.Message);}
            catch (AdminNotFoundException ex){ Console.WriteLine(ex.Message);}
            catch (NullUserException ex) { Console.WriteLine(ex.Message); }
            catch (UserNotFoundException ex) { Console.WriteLine(ex.Message); }
        }

        public void NewRegistration()
        {
            Console.WriteLine("Selectati tipul de utilizator pentru inregistrare: Admin, Client, Utilizator");
            string tip = Console.ReadLine().ToLower();

            switch (tip)
            {
                case "Admin":
                    RegisterAdmin();
                    break;
                case "Customer":
                    RegisterCustomer();
                    break;
                default:
                    Console.WriteLine("Tip utilizator invalid.");
                    break;
            }
        }

        private void RegisterAdmin()
        {
            string type = "Admin";

            Console.WriteLine("Introduceti numele de utilizator:");
            string username = Console.ReadLine();

            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti emailul:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string password = Console.ReadLine();

            Console.WriteLine("Introduceti adresa:");
            string billingAddress = Console.ReadLine();

            try
            {
                int id = _userRepository.GenerateId();
                Admin admin = new Admin(id, type, username, fullName, email, password, billingAddress);
                _userRepository.AddUser(admin);
                Console.WriteLine($"Admin înregistrat cu succes! ID-ul dumneavoastră este: {id}");
            }
            catch (NullAdminException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RegisterCustomer()
        {
            string type = "Customer";

            Console.WriteLine("Introduceti numele de utilizator:");
            string username = Console.ReadLine();

            Console.WriteLine("Introduceti numele complet:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti emailul:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string password = Console.ReadLine();

            Console.WriteLine("Introduceti adresa:");
            string billingAddress = Console.ReadLine();

            try
            {
                int id = _userRepository.GenerateId();
                Customer customer = new Customer(id, type, username, fullName, email, password, billingAddress);
                _userRepository.AddUser(customer);
                Console.WriteLine($"Client înregistrat cu succes! ID-ul dumneavoastră este: {id}");
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
                case "Admin":
                    try
                    {
                        Admin admin = (Admin)_userQueryService.FindUserById(id);
                        if (admin != null)
                        {
                            admin.Password = parolaNoua;
                            _userRepository.UpdateUser(id, admin);
                            Console.WriteLine("Parola actualizată cu succes pentru Admin.");
                        }
                        else
                        {
                            Console.WriteLine("Adminul nu a fost găsit.");
                        }
                    }
                    catch (AdminNotFoundException ex) { Console.WriteLine(ex.Message); }
                    catch (NullAdminException ex) { Console.WriteLine(ex.Message); }
                    break;

                case "Customer":
                    try
                    {
                        Customer customer = (Customer)_userQueryService.FindUserById(id);
                        if (customer != null)
                        {
                            customer.Password = parolaNoua;
                            _userRepository.UpdateUser(id, customer);
                            Console.WriteLine("Parola actualizată cu succes pentru Client.");
                        }
                        else
                        {
                            Console.WriteLine("Clientul nu a fost găsit.");
                        }
                    }
                    catch (CustomerNotFoundException ex) { Console.WriteLine(ex.Message); }
                    catch (NullCustomerException ex) { Console.WriteLine(ex.Message); }
                    break;
            }

        }



    }
}
