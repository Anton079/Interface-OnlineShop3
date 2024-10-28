using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop.Products.Service;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop.Customers.Service;
using System;
using System.Collections.Generic;
using Interface_OnlineShop.OrderDetails.Service;
using Interface_OnlineShop.Orders.Service;

namespace Interface_OnlineShop3
{
    public class View
    {
        private IOrdersQueryService _ordersQueryService;
        private IOrdersCommandService _ordersCommandService;

        private IOrderDetailsQueryService _orderDetailsQueryService;
        private IOrderDetailsCommandService _orderDetailsCommandService;

        private ICustomerQueryService _customerQueryService;
        private ICustomerCommandService _customerCommandService;

        private IProductQueryService _productQueryService;
        private IProductComandService _productComandService;

        public View(IOrdersQueryService ordersQueryService, IOrdersCommandService ordersCommandService,
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

        public void Meniu()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Selectati o optiune:");

            Console.WriteLine("\n--- Comenzi ---");
            Console.WriteLine("1.  Afisati toate comenzile");
            Console.WriteLine("2.  Afisati detalii despre comenzi");
            Console.WriteLine("3.  Adaugati o noua comanda");
            Console.WriteLine("4.  Actualizati o comanda");
            Console.WriteLine("5.  Afisati toate detaliile comenzii");

            Console.WriteLine("\n--- Clienti ---");
            Console.WriteLine("6.  Afisati toti clientii");
            Console.WriteLine("7.  Adaugati un client");
            Console.WriteLine("8.  Actualizati un client");
            Console.WriteLine("9.  Stergeti un client");

            Console.WriteLine("\n--- Produse ---");
            Console.WriteLine("10. Afisati toate produsele");
            Console.WriteLine("11. Adaugati un produs");
            Console.WriteLine("12. Actualizati un produs");
            Console.WriteLine("13. Stergeti un produs");

            Console.WriteLine("\n14. Iesire");
        }

        public void Play()
        {
            bool running = true;
            while (running)
            {
                Meniu();
                string alegere = Console.ReadLine();

                switch (alegere)
                {
                    case "1":
                        AfisareComenzi();
                        break;
                    case "2":
                        AfisareDetaliiComenzi();
                        break;
                    case "3":
                        AdaugaComanda();
                        break;
                    case "4":
                        ActualizeazaComanda();
                        break;
                    case "5":
                        AfisareDetaliiComanda();
                        break;
                    case "6":
                        AfisareClienti();
                        break;
                    case "7":
                        AdaugaClient();
                        break;
                    case "8":
                        ActualizeazaClient();
                        break;
                    case "9":
                        StergeClient();
                        break;
                    case "10":
                        AfisareProduse();
                        break;
                    case "11":
                        AdaugaProdus();
                        break;
                    case "12":
                        ActualizeazaProdus();
                        break;
                    case "13":
                        StergeProdus();
                        break;
                    case "14":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Optiune necunoscuta.");
                        break;
                }
            }
        }

        // --- Comenzi Functions ---
        public void AfisareComenzi()
        {
            List<Order> comenzi = _ordersQueryService.GetAllOrders();
            foreach (Order x in comenzi)
            {
                Console.WriteLine(x);
            }
        }

        public void AfisareDetaliiComenzi()
        {
            List<OrderDetail> detaliiComenzi = _orderDetailsQueryService.GetAllOrderDetails();
            foreach (OrderDetail x in detaliiComenzi)
            {
                Console.WriteLine(x);
            }
        }

        public void AdaugaComanda()
        {
            Console.WriteLine("Introduceti ID-ul comenzii:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti ID-ul clientului:");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti suma comenzii:");
            int amount = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti adresa de livrare:");
            string shippingAddress = Console.ReadLine();

            Order newOrder = new Order(id, customerId, amount, shippingAddress);
            _ordersCommandService.AddOrders(newOrder);

            Console.WriteLine("Comanda a fost adaugata cu succes.");
        }

        public void ActualizeazaComanda()
        {
            Console.WriteLine("Introduceti ID-ul comenzii de actualizat:");
            int id = int.Parse(Console.ReadLine());

            Order comanda = _ordersQueryService.FindOrdersById(id);
            if (comanda != null)
            {
                Console.WriteLine("Introduceti noua adresa de livrare:");
                string shippingAddress = Console.ReadLine();
                comanda.ShippingAddress = shippingAddress;

                _ordersCommandService.UpdateOrders(id,comanda);
                Console.WriteLine("Comanda a fost actualizata cu succes.");
            }
            else
            {
                Console.WriteLine("Comanda cu acest ID nu a fost gasita.");
            }
        }

        public void AfisareDetaliiComanda()
        {
            Console.WriteLine("Introduceti ID-ul comenzii pentru detalii:");
            int orderId = int.Parse(Console.ReadLine());

            List<OrderDetail> detaliiComanda = _orderDetailsQueryService.GetAllOrderDetails();
            foreach (OrderDetail detaliu in detaliiComanda)
            {
                if (detaliu.OrderId == orderId)
                {
                    Console.WriteLine(detaliu);
                }
            }
        }

        // --- Clienti Functions ---
        public void AfisareClienti()
        {
            List<Customer> clienti = _customerQueryService.GetAllCustomers();
            foreach (Customer client in clienti)
            {
                Console.WriteLine(client);
            }
        }

        public void AdaugaClient()
        {
            Console.WriteLine("Introduceti ID-ul clientului:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti email-ul clientului:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti parola clientului:");
            string password = Console.ReadLine();

            Console.WriteLine("Introduceti numele complet al clientului:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Introduceti adresa de facturare:");
            string billingAddress = Console.ReadLine();

            Customer newCustomer = new Customer(id, email, password, fullName, billingAddress);
            _customerCommandService.AddCustomer(newCustomer);

            Console.WriteLine("Clientul a fost adaugat cu succes.");
        }

        public void ActualizeazaClient()
        {
            Console.WriteLine("Introduceti ID-ul clientului de actualizat:");
            int id = int.Parse(Console.ReadLine());

            Customer client = _customerQueryService.FindCustomerById(id);
            if (client != null)
            {
                Console.WriteLine("Introduceti noul email:");
                client.Email = Console.ReadLine();

                Console.WriteLine("Introduceti noua parola:");
                client.Password = Console.ReadLine();

                Console.WriteLine("Introduceti noul nume complet:");
                client.FullName = Console.ReadLine();

                Console.WriteLine("Introduceti noua adresa de facturare:");
                client.BillingAddress = Console.ReadLine();

                _customerCommandService.UpdateCustomer(id,client);
                Console.WriteLine("Clientul a fost actualizat cu succes.");
            }
            else
            {
                Console.WriteLine("Clientul cu acest ID nu a fost gasit.");
            }
        }

        public void StergeClient()
        {
            Console.WriteLine("Introduceti ID-ul clientului de sters:");
            int id = int.Parse(Console.ReadLine());

            Customer client = _customerQueryService.FindCustomerById(id);
            if (client != null)
            {
                _customerCommandService.RemoveCustomer(client.Id);
                Console.WriteLine("Clientul a fost sters cu succes.");
            }
            else
            {
                Console.WriteLine("Clientul cu acest ID nu a fost gasit.");
            }
        }

        // --- Produse Functions ---
        public void AfisareProduse()
        {
            List<Product> list = _productQueryService.getAll();

            if (list == null)
            {
                Console.WriteLine("Nu exista produse disponibile.");
                return;
            }

            foreach (Product x in list)
            {
                Console.WriteLine(x);
            }
        }


        public void AdaugaProdus()
        {
            Console.WriteLine("Introduceti ID-ul produsului:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti numele produsului:");
            string name = Console.ReadLine();

            Console.WriteLine("Introduceti pretul produsului:");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti descrierea produsului:");
            string description = Console.ReadLine();

            Console.WriteLine("Introduceti data crearii produsului (format: yyyy-MM-dd):");
            int createDate = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti stocul produsului:");
            int stock = int.Parse(Console.ReadLine());

            Product newProduct = new Product(id, name, price, description, createDate, stock);
            _productComandService.AddProduct(newProduct);

            Console.WriteLine("Produsul a fost adaugat cu succes.");
        }

        public void ActualizeazaProdus()
        {
            Console.WriteLine("Introduceti ID-ul produsului de actualizat:");
            int idWanted = int.Parse(Console.ReadLine());

            Product produs = _productQueryService.FindProductById(idWanted);
            if (produs != null)
            {
                bool updating = true;

                while (updating)
                {
                    Console.WriteLine("Ce vrei sa editezi?");
                    Console.WriteLine("1. Nume");
                    Console.WriteLine("2. Pret");
                    Console.WriteLine("3. Descriere");
                    Console.WriteLine("4. Data crearii");
                    Console.WriteLine("5. Stoc");
                    Console.WriteLine("6. Iesire");

                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Introduceti noul nume:");
                            produs.Name = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Introduceti noul pret:");
                            produs.Price = int.Parse(Console.ReadLine());
                            break;
                        case 3:
                            Console.WriteLine("Introduceti noua descriere:");
                            produs.Descriptions = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Introduceti noua data de creare (format: yyyy-MM-dd):");
                            produs.CreateDate = int.Parse(Console.ReadLine());
                            break;
                        case 5:
                            Console.WriteLine("Introduceti noul stoc:");
                            produs.Stock = int.Parse(Console.ReadLine());
                            break;
                        case 6:
                            updating = false;
                            break;
                        default:
                            Console.WriteLine("Optiune nevalida.");
                            break;
                    }
                }

                Product newProduct = new Product(idWanted,produs.Name, produs.Price, produs.Descriptions, produs.CreateDate,produs.Stock);

                _productComandService.UpdateProduct(idWanted, newProduct);
                Console.WriteLine("Produsul a fost actualizat cu succes.");
            }
            else
            {
                Console.WriteLine("Produsul cu acest ID nu a fost gasit.");
            }
        }

        public void StergeProdus()
        {
            Console.WriteLine("Introduceti ID-ul produsului de sters:");
            int id = int.Parse(Console.ReadLine());

            Product produs = _productQueryService.FindProductById(id);
            if (produs != null)
            {
                _productComandService.RemoveProduct(id);
                Console.WriteLine("Produsul a fost sters cu succes.");
            }
            else
            {
                Console.WriteLine("Produsul cu acest ID nu a fost gasit.");
            }
        }
    }
}
