using Interface_OnlineShop3.OrderDetails.Exceptions;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Exceptions;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Exceptions;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using Interface_OnlineShop3.System.Exceptions;
using Interface_OnlineShop3.Users.Exceptions;
using Interface_OnlineShop3.Users.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3
{
    public class MainView
    {
        private Customer _customer;
        private IOrdersCommandService _ordersCommandService;
        private IOrdersQueryService _ordersQueryService;

        private IOrderDetailsCommandService _orderDetailsCommandService;
        private IOrderDetailsQueryService _orderDetailsQueryService;

        private IProductQueryService _productQueryService;

        private ICos _cos;

        public MainView(Customer customer,  ICos cos, IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService, IProductQueryService productQueryService, IOrdersQueryService ordersQueryService, IOrderDetailsQueryService orderDetailsQueryService)
        {
            _customer = customer;
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
            _cos = cos;
        }

        public void MeniuView()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("         MENIU         ");
            Console.WriteLine("=========================");
            Console.WriteLine("1. Afisare produse");
            Console.WriteLine("2. Afisare cos");
            Console.WriteLine("3. Adauga produse in cos");
            Console.WriteLine("4. Modifica cantitatea unui produs din cos");
            Console.WriteLine("5. Sterge un produs din cos");
            Console.WriteLine("6. Plasare comanda");
            Console.WriteLine("7. Istoricul comenzilor tale");
            Console.WriteLine("8. Cautare produs");
            Console.WriteLine("9. Filtrare produse dupa pret");
            Console.WriteLine("10. Iesire");
            Console.WriteLine("=========================");
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
                        ViewOrderHistory();
                        break;

                    case "8":
                        SearchProduct();
                        break;

                    case "9":
                        FilterProductsByPrice();
                        break;

                    case "10":
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


        //afisare produse
        public void AfisareProduse()
        {
            Console.WriteLine("    PRODUSE DISPONIBILE   ");

            List<Product> products = _productQueryService.getAll();

            foreach(Product product in products)
            {
                Console.WriteLine($"Nume: {product.Name} | Descriere: {product.Descriptions} | Stoc: {product.Stock}");
            }
            Console.WriteLine("=========================");
        }

        //Afisare Cos
        public void AfisareCos()
        {
            Console.WriteLine("         COSUL TAU        ");
            int total = 0;

            foreach (OrderDetailsDto x in _cos.GetAll())
            {
                int productPrice = _productQueryService.FindProductPriceByName(x.ProductName);
                int subtotal = productPrice * x.Quantity;
                total += subtotal;

                Console.WriteLine($"Produs ID: {x.ProductId}, Produs nume: {x.ProductName}, Cantitate: {x.Quantity}, Pret unitar: {productPrice}, Subtotal: {subtotal}");
            }

            Console.WriteLine($"Total cos: {total} lei.");
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

            try
            {
                int productId = _productQueryService.FindProductIdByName(productName);
                int productStockAvailable = _productQueryService.FindProductStockByName(productName);

                Console.WriteLine("Ce cantitate doresti");
                int quantityWanted = Int32.Parse(Console.ReadLine());

                if (quantityWanted > productStockAvailable)
                {
                    Console.WriteLine("Cantitea este mai mare decat ce-a disponibila!");
                }

                int price = _productQueryService.FindProductPriceByName(productName);

                OrderDetailsDto newProduct = new OrderDetailsDto(productId, productName, quantityWanted, price);
                _cos.AddCos(newProduct);

            }catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }catch(QuantityProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Produsul a fost adaugat cu succes!");
        }

        //update
        public void EditQuantity()
        {
            Console.WriteLine("Introdu numele produsului: ");
            string productName = Console.ReadLine();
            Console.WriteLine("Introdu noua cantiate: ");
            int newQuantity = int.Parse(Console.ReadLine());

            try
            {
                int productStockAvailable = _productQueryService.FindProductStockByName(productName);

                if (newQuantity > productStockAvailable)
                {
                    Console.WriteLine($"Cantitea dorita este mai mare decat ce-a disponibila, tu ai ales {newQuantity}, in stock se afla maxim {productStockAvailable}!");
                }

                _cos.EditQuantity(productName, newQuantity);
            }catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }catch (QuantityProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Ștergere produs din coș
        public void RemoveFromCos()
        {
            Console.WriteLine("Introduceți numele produsului pe care doriți să îl ștergeți din coș:");
            string productName = Console.ReadLine();

            try
            {
                _cos.RemoveFromCos(productName);
            }
            catch (RemoveFromCosException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //plasare comanda din cos
        public void PlaceOrder()
        {
            if (_cos.GetAll().Count == 0)
            {
                Console.WriteLine("Cosul este gol, trebuie adaugate produse pentru a plasa comanda!");
                return;
            }

            try
            {
                int customerId = _customer.Id;
                string customerAddress = _customer.BillingAddress;
            
                _ordersCommandService.PlaceOrder(_cos.GetAll(), customerId, customerAddress);
                _cos.Clear();
            }
            catch(CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        //istoric comenzi
        public void ViewOrderHistory()
        {
            List<Order> orders = _ordersQueryService.GetAllOrders();
            List<OrderDetail> orderDetails = _orderDetailsQueryService.GetAllOrderDetails();

            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine("Nu ati comandat nimic pentru a afisa istoricul comenzilor!");
                return;
            }

            Console.WriteLine("    PRODUSELE PE CARE LE-AI COMANDAT   ");

            // Afișăm comenzile disponibile
            foreach (Order order in orders)
            {
                Console.WriteLine();
                Console.WriteLine($"Comanda cu ID: {order.Id}, cu totalul de: {order.Amount}!");
                try
                {
                    foreach (OrderDetail detail in orderDetails)
                    {
                        if (detail.OrderId == order.Id)
                        {
                            string productName = _productQueryService.FindProductNameById(detail.ProductId);
                            Console.Write($"{productName}, ");
                        }
                    }
                }catch(ProductNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Introduceti ID-ul comenzii pentru a vedea mai multe detalii sau apasati 0 pentru a iesi: ");

            
            string input = Console.ReadLine();
            int selectedOrderId = 0;

            try
            {
                selectedOrderId = int.Parse(input);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Eroare la conversie intre int si string");
            }

            if (selectedOrderId == 0)
            {
                Console.WriteLine("Iesire din istoricul comenzilor.");
              
            }

            try
            {
                
                Order selectedOrder = null;
                foreach (Order order in orders)
                {
                    if (order.Id == selectedOrderId)
                    {
                        selectedOrder = order;
                        break;
                    }
                }

                if (selectedOrder == null)
                {
                    Console.WriteLine("ID-ul comenzii introdus nu exista!");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Detalii pentru comanda cu ID: {selectedOrder.Id}, cu totalul de: {selectedOrder.Amount}!");

                foreach (OrderDetail detail in orderDetails)
                {
                    if (detail.OrderId == selectedOrder.Id)
                    {
                        string productName = _productQueryService.FindProductNameById(detail.ProductId);
                        Console.WriteLine($"Produs ORDER ID: {detail.Id}, PRODUS: {productName}, PRETUL: {detail.Price}, CANTITATEA DE: {detail.Quantity}");
                    }
                }
            }
            catch(OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }catch(ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //afiseaza o lista filtrara cu produse dupa name product introdus de tine
        public void SearchProduct()
        {
            Console.WriteLine("Introduceti numele sau un cuvant cheie pentru a cauta un produs: ");
            string searchTerm = Console.ReadLine();

            try
            {
                List<Product> products = _productQueryService.FindProductsByName(searchTerm);
                foreach (Product x in products)
                {
                    Console.WriteLine(x.Name, x.Price, x.Descriptions);
                }
            }catch(QuantityProductNotFoundException ex) { Console.WriteLine(ex.Message); }

        }

        //afiseaza o lista filtrara cu price ul de la tine
        public void FilterProductsByPrice()
        {
            Console.WriteLine("Introduceti pretul minim:");
            int minPrice = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti pretul maxim:");
            int maxPrice = int.Parse(Console.ReadLine());

            try
            {
                List<Product> products = _productQueryService.FindMinAndMaxProductsPrice(minPrice, maxPrice);
                foreach (Product x in products)
                {
                    Console.WriteLine(x.Name, x.Price, x.Descriptions);
                }
            }
            catch (QuantityProductNotFoundException ex) { Console.WriteLine(ex.Message); }
        }

    }
}
