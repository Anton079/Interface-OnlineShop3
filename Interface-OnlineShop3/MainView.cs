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
            Console.WriteLine("Produsul a fost adaugat cu succes!");
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
            if (_cos.GetAll().Count == 0)
            {
                Console.WriteLine("Cosul este gol, trebuie adaugate produse pentru a plasa comanda!");
                return;
            }

            try
            {
                // Creeaza o lista temporara pentru a stoca obiectele din cos
                List<OrderDetailsDto> detailsDtos = _cos.GetAll().ToList();

                foreach (OrderDetailsDto orderDto in _cos.GetAll())
                {
                    int customerId = customer.Id;
                    string customerAddress = customer.BillingAddress;

                    _ordersCommandService.PlaceOrder(detailsDtos, customerId, customerAddress);
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

            foreach (Order order in orders)
            {
                Console.WriteLine();
                Console.WriteLine($"Comanda cu ID: {order.Id}, cu totalul de: {order.Amount}!");

                // Filtrăm detaliile comenzii pentru această comandă fără expresii lambda
                List<OrderDetail> detailsForOrder = new List<OrderDetail>();
                foreach (OrderDetail detail in orderDetails)
                {
                    if (detail.OrderId == order.Id)
                    {
                        detailsForOrder.Add(detail);
                    }
                }

                foreach (OrderDetail detail in detailsForOrder)
                {
                    string productName = _productQueryService.FindProductNameById(detail.ProductId);
                    Console.WriteLine($"Produs ORDER ID:{detail.Id}, PRODUS: {productName}, PRETUL: {detail.Price}, CANTITATEA DE: {detail.Quantity}");
                }
            }
        }


        //afiseaza o lista filtrara cu produse dupa name product introdus de tine
        public void SearchProduct()
        {
            Console.WriteLine("Introduceti numele sau un cuvant cheie pentru a cauta un produs: ");
            string searchTerm = Console.ReadLine().ToLower();

            List<Product> products = _productQueryService.getAll();
            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (product.Name.ToLower().Contains(searchTerm) || product.Descriptions.ToLower().Contains(searchTerm))
                {
                    filteredProducts.Add(product);
                }
            }

            if (filteredProducts.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit produse care sa corespunda cautarii ");
                return;
            }

            Console.WriteLine("Produsele gasite: ");
            foreach (Product product in filteredProducts)
            {
                Console.WriteLine("Nume: " + product.Name + " | Descriere: " + product.Descriptions + " | Stoc: " + product.Stock);
            }
        }

        //afiseaza o lista filtrara cu price ul de la tine
        public void FilterProductsByPrice()
        {
            Console.WriteLine("Introduceti pretul minim:");
            int minPrice = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti pretul maxim:");
            int maxPrice = int.Parse(Console.ReadLine());

            List<Product> products = _productQueryService.getAll();
            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (product.Price >= minPrice && product.Price <= maxPrice)
                {
                    filteredProducts.Add(product);
                }
            }

            if (filteredProducts.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit produse in aceasta gama de pret.");
                return;
            }

            Console.WriteLine("Produsele in aceasta gama de pret:");
            foreach (Product product in filteredProducts)
            {
                Console.WriteLine("Nume: " + product.Name + " | Pret: " + product.Price + " | Stoc: " + product.Stock);
            }
        }

    }
}
