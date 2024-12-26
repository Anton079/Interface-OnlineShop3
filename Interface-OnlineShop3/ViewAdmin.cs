using Interface_OnlineShop3.Admins.Models;
using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3
{
    public class ViewAdmin
    {
        private Admin _admin;
        private IProductRepository _productRepository;
        private IProductComandService _productComandService;
        private IProductQueryService _productQueryService;

        private IOrdersCommandService _ordersCommandService;
        private IOrdersQueryService _ordersQueryService;

        private IOrderDetailsCommandService _orderDetailsCommandService;
        private IOrderDetailsQueryService _orderDetailsQueryService;

        public ViewAdmin(
            Admin admin,
            IProductRepository productRepository,
            IProductComandService productComandService,
            IOrdersCommandService ordersCommandService,
            IOrderDetailsCommandService orderDetailsCommandService,
            IProductQueryService productQueryService,
            IOrdersQueryService ordersQueryService,
            IOrderDetailsQueryService orderDetailsQueryService)
        {
            _admin = admin;
            _productRepository = productRepository;
            _productComandService = productComandService;
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
        }

        public void MeniuAdmin()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("        MENIU ADMIN       ");
            Console.WriteLine("=========================");
            Console.WriteLine("1. Vizualizare toate comenzile");
            Console.WriteLine("2. Vizualizare detalii comenzi");
            Console.WriteLine("3. Gestionare produse");
            Console.WriteLine("4. Iesire");
            Console.WriteLine("=========================");
        }

        public void Play()
        {
            bool running = true;

            while (running)
            {
                MeniuAdmin();
                string alegere = Console.ReadLine()?.Trim();

                switch (alegere)
                {
                    case "1":
                        ViewAllOrders();
                        break;

                    case "2":
                        ViewOrderDetails();
                        break;

                    case "3":
                        ManageProducts();
                        break;

                    case "4":
                        Console.WriteLine("La revedere!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Optiune invalida. Incercati din nou.");
                        break;
                }

                Console.WriteLine();
            }
        }

        // Vizualizarea tuturor comenzilor
        public void ViewAllOrders()
        {
            List<Order> orders = _ordersQueryService.GetAllOrders();

            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine("Nu exista comenzi disponibile.");
                return;
            }

            Console.WriteLine("Comenzile existente:");
            foreach (Order order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Client ID: {order.CustomerId}, Total: {order.Amount}");
            }
        }

        // Vizualizarea detaliilor pentru o comandă
        public void ViewOrderDetails()
        {
            Console.WriteLine("Introduceti ID-ul comenzii pentru a vizualiza detaliile:");
            int orderId = int.Parse(Console.ReadLine());

            List<OrderDetail> orderDetails = _orderDetailsQueryService.GetAllOrderDetails();

            Console.WriteLine($"Detalii pentru comanda ID: {orderId}");
            foreach (OrderDetail detail in orderDetails)
            {
                if (detail.OrderId == orderId)
                {
                    string productName = _productQueryService.FindProductNameById(detail.ProductId);
                    Console.WriteLine($"Produs: {productName}, Cantitate: {detail.Quantity}, Pret: {detail.Price}");
                }
            }
        }

        // Gestionarea produselor
        public void ManageProducts()
        {
            Console.WriteLine("1. Adaugare produs");
            Console.WriteLine("2. Modificare produs");
            Console.WriteLine("3. Stergere produs");
            Console.WriteLine("Alege optiunea:");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    EditProduct();
                    break;
                case "3":
                    RemoveProduct();
                    break;
                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }

        // Adaugare produs
        private void AddProduct()
        {
            int generatorId = _productRepository.GenerateId();

            Console.WriteLine("Introduceti numele produsului:");
            string name = Console.ReadLine();

            Console.WriteLine("Introduceti descrierea produsului:");
            string description = Console.ReadLine();

            Console.WriteLine("Introduceti pretul produsului:");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti data produsului:");
            int dateProduct = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti stocul produsului:");
            int stock = int.Parse(Console.ReadLine());

            Product newProduct = new Product(generatorId, name, price, description, dateProduct, stock);
            _productComandService.AddProduct(newProduct);

            Console.WriteLine("Produsul a fost adaugat cu succes!");
        
        }

        private void EditProduct()
        {
            Console.WriteLine("Introduceti ID-ul produsului pe care doriti sa il modificati:");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("Ce doriti sa modificati?");
            Console.WriteLine("1. Nume");
            Console.WriteLine("2. Pret");
            Console.WriteLine("3. Descriere");
            Console.WriteLine("4. Create Date");
            Console.WriteLine("5. Stoc");
            string option = Console.ReadLine();

            Product productToEdit = _productQueryService.FindProductById(productId);

            switch (option)
            {
                case "nume":
                    Console.WriteLine("Introduceti noul nume:");
                    string newName = Console.ReadLine();
                    productToEdit.Name = newName;
                    break;

                case "pret":
                    Console.WriteLine("Introduceti noul pret:");
                    int newPrice = Int32.Parse(Console.ReadLine());
                    productToEdit.Price = newPrice;
                    break;

                case "descriere":
                    Console.WriteLine("Introduceti noua descriere:");
                    string newDescription = Console.ReadLine();
                    productToEdit.Descriptions = newDescription;
                    break;

                case "create date":
                    Console.WriteLine("Introduceti noul create date:");
                    int newCreateDate = Int32.Parse(Console.ReadLine());
                    productToEdit.CreateDate = newCreateDate;
                    break;

                case "stoc":
                    Console.WriteLine("Introduceti noul stoc:");
                    int newStock = Int32.Parse(Console.ReadLine());
                    productToEdit.Stock = newStock;
                    break;

                default:
                    Console.WriteLine("Optiune invalida. Operatia a fost anulata.");
                    return;
            }

            _productComandService.UpdateProduct(productId, productToEdit);
            Console.WriteLine("Produsul a fost modificat cu succes!");
        }


        // Ștergere produs
        private void RemoveProduct()
        {
            Console.WriteLine("Introduceti ID-ul produsului pe care doriti sa il stergeti:");
            int productId = int.Parse(Console.ReadLine());

            _productComandService.RemoveProduct(productId);
            Console.WriteLine("Produsul a fost sters cu succes!");
        }
    }
}
