using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Models;
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
    public class MainView
    {
        private IOrdersCommandService _ordersCommandService;
        private IOrderDetailsCommandService _orderDetailsCommandService;
        private IProductQueryService _productQueryService;
        private IOrdersQueryService _ordersQueryService;
        private IOrderDetailsQueryService _orderDetailsQueryService;
        private ICos _cos;

        private Customer customer = new Customer(1, "anton@gmail.com", "raul", "manolache", "Cluj");

        public MainView(ICos cos, IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService, IProductQueryService productQueryService, IOrdersQueryService ordersQueryService, IOrderDetailsQueryService orderDetailsQueryService)
        {
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
            _cos = cos;
        }

        public void MeniuView()
        {
            Console.WriteLine("1. Afisare produse");
            Console.WriteLine("2. Afisare cos");
            Console.WriteLine("3. Adauga produse în cos");
            Console.WriteLine("4. Modifica cantitatea unui produs din cos");
            Console.WriteLine("5. Sterge un produs din cos");
            Console.WriteLine("6. Plasare comanda");
            Console.WriteLine("7. Iesire");
        }

        public void Play()
        {
            bool running = true;

            while (running)
            {
                MeniuView();
                string alegere = Console.ReadLine()?.Trim();

                switch (alegere)
                {
                    case "1":
                        AfisareProduse();
                        break;

                    case "2":
                        AfisareCos();
                        break;

                    case "3":
                        AddInCos();
                        break;

                    case "4":
                        EditQuantity();
                        break;

                    case "5":
                        RemoveFromCos();
                        break;

                    case "6":
                        PlaceOrder();
                        break;

                    case "7":
                        Console.WriteLine("La revedere!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Optiune invalida. Încercati din nou.");
                        break;
                }

                Console.WriteLine();
            }
        }

        //afisare produse
        public void AfisareProduse()
        {
            List<Product> products = _productQueryService.getAll();

            foreach(Product product in products)
            {
                Console.WriteLine($"{product.Name},{product.Descriptions},{product.Stock}");
            }
        }

        //Afisare Cos
        public void AfisareCos()
        {
            Console.WriteLine("IN COS AI:");
            foreach (OrderDetailsDto x in _cos.GetAll())
            {
                Console.WriteLine($"Produs ID: {x.ProductId}, Produs nume: {x.ProductName}, Cantitate: {x.Quantity}");
            }
            Console.WriteLine(" ");

        }

        //Adaugare in Cos
        public void AddInCos()
        {
            Console.WriteLine("Produsele disponibile!");
            AfisareProduse();
            Console.WriteLine("=================================");

            Console.WriteLine("Introduce numele produsului pe care vrei sa il aduagi in cos!");
            string productWanted = Console.ReadLine();

            string productName = productWanted.ToLower();

            int productId = _productQueryService.FindProductIdByName(productName);
            int productStockAvailable = _productQueryService.FindProductStockByName(productName);

            Console.WriteLine("Ce cantitate doresti");
            int quantityWanted = Int32.Parse(Console.ReadLine());

            if (quantityWanted > productStockAvailable)
            {
                Console.WriteLine("Cantitea este mai mare decat ce-a disponibila!");
            }

            OrderDetailsDto newProduct = new OrderDetailsDto(productId, productName, quantityWanted);
            _cos.AddCos(newProduct);
        }

        //update
        public void EditQuantity()
        {
            bool edited = false;

            Console.WriteLine("Introdu numele produsului: ");
            string productName = Console.ReadLine();
            Console.WriteLine("Introdu noua cantiate: ");
            int newQuantity = int.Parse(Console.ReadLine());

            int productStockAvailable = _productQueryService.FindProductStockByName(productName);

            if (newQuantity > productStockAvailable)
            {
                Console.WriteLine($"Cantitea este mai mare decat ce-a disponibila, tu ai ales {newQuantity}, in stock se afla maxim {productStockAvailable}!");
            }
            if(edited)
            {
                Console.WriteLine("Cantitatea din cos s-a modificat!");
                edited = _cos.EditQuantity(productName, newQuantity);
            }
            else
            {
                Console.WriteLine("Cantitatea din cos nu s-a modificat!");
            }
        }

        // Ștergere produs din coș
        public void RemoveFromCos()
        {
            Console.WriteLine("Introduceți numele produsului pe care doriți să Il StergeTi din cos:");
            string productName = Console.ReadLine();

            bool removed = _cos.RemoveFromCos(productName);
            if (removed)
            {
                Console.WriteLine("Produsul a fost șters din coș!");
            }
            else
            {
                Console.WriteLine("Produsul nu a fost găsit în coș!");
            }
        }

        //plasare comanda din cos
        public void PlaceOrder()
        {
            if(_cos.GetAll().Count == 0)
            {
                Console.WriteLine("Cosul este gol, trebuie adaugate produse pentru a plasa comanda!");
                return;
            }
    
            try
            {
                foreach(OrderDetailsDto orderDto in _cos.GetAll())
                {
                    Order newOrder = new Order(orderDto.ProductId, customer.Id, orderDto.Quantity, customer.BillingAddress);
                    _ordersCommandService.AddOrders(newOrder);

                    int productPrice = _productQueryService.FindProductPriceByName(orderDto.ProductName);
                    int idNewGen = _orderDetailsQueryService.GenerateId();

                    OrderDetail newOrderDetail = new OrderDetail(idNewGen, newOrder.Id, orderDto.ProductId, productPrice, orderDto.Quantity);
                    _orderDetailsCommandService.AddOrderDetails(newOrderDetail);

                    Console.WriteLine($"Comanda cu datele Id OrderDetail:{idNewGen}, Id Order{newOrder.Id}, Id Product:{orderDto.ProductId}, Product Price:{productPrice}, Quantity:{orderDto.Quantity} a fost plasata");
                }

                Console.WriteLine("Cosul a fost golit!");
                _cos.Clear();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Comanda nu a fost plasata");
                Console.WriteLine(ex);
            }
        }


    }
}
