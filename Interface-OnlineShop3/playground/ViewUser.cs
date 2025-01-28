using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.playground
{
    public class ViewUser
    {
        private Customer _customer;
        private IOrdersCommandService _ordersCommandService;
        private IOrderDetailsCommandService _orderDetailsCommandService;
        private IProductQueryService _productQueryService;
        private IOrdersQueryService _ordersQueryService;
        private IOrderDetailsQueryService _orderDetailsQueryService;

        private List<int> cos;

        public ViewUser(Customer customer, IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService, IProductQueryService productQueryService, IOrdersQueryService ordersQueryService, IOrderDetailsQueryService orderDetailsQueryService)
        {
            _customer = customer;
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
            cos = new List<int>();
        }

        public void MeniuUser()
        {
            Console.WriteLine("1. Afisare cos");
            Console.WriteLine("2. Adauga produse in cos");
            Console.WriteLine("3. Plaseaza Comanda");
            Console.WriteLine("4. Editeaza comanda");
            Console.WriteLine("5. Afiseaza toate comenzile tale");
            Console.WriteLine("6. Afiseaza toate detaliitle comenzilor tale");
        }

        public void Play()
        {
            bool running = true;
            while (running)
            {
                MeniuUser();
                string alegere = Console.ReadLine();

                switch (alegere)
                {
                    case "1":
                        ShowCart();
                        break;
                    case "2":
                        AddProductsToOrder();
                        break;
                    case "3":
                        PlaceOrder();
                        break;
                    case "4":
                        EditOrder();
                        break;
                    case "5":
                        ShowAllOrders();
                        break;
                    case "6":
                        AfisareDetaliiComenzi();
                        break;
                    default:
                        running = false;
                        break;
                }
            }
        }

        public void AfisareDetaliiComenzi()
        {
            List<OrderDetail> detaliiComenzi = _orderDetailsQueryService.GetAllOrderDetails();
            List<Order> allOrders = _ordersQueryService.GetAllOrders();

            if (allOrders == null)
            {
                Console.WriteLine("Nu ai comenzi pe care sa le afisam!");
            }

            Console.WriteLine("=====Comenziile tale=======");
            foreach (OrderDetail orderDetail in detaliiComenzi)
            {
                foreach (Order order in allOrders)
                {
                    if (orderDetail.OrderId == order.Id)
                    {

                        Console.WriteLine($"Id ul comenzii:{orderDetail.OrderId}, Cantitatea:{orderDetail.Quantity}, Pret:{orderDetail.Price} ");
                    }
                }
            }
            Console.WriteLine("===========================");

        }

        public void ShowAllOrders()
        {
            List<Order> allOrders = _ordersQueryService.GetAllOrders();
            List<Order> customerOrders = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.CustomerId == _customer.Id)
                {
                    customerOrders.Add(order);
                }
            }

            Console.WriteLine("Comenzile tale:");
            foreach (Order order in customerOrders)
            {
                Console.WriteLine($"ID: {order.Id}, Total: {order.Amount}, Adresa: {order.ShippingAddress}");
            }
        }

        public void ShowCart()
        {
            Console.WriteLine("Produsele in cos:");
            foreach (int productId in cos)
            {
                Product product = _productQueryService.FindProductById(productId);
                if (product != null)
                {
                    Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}");
                }
            }
        }

        public void AddProductsToOrder()
        {
            Console.WriteLine("Produsele disponibile:");
            List<Product> products = _productQueryService.getAll();

            foreach (Product product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}, Stoc: {product.Stock}");
            }

            Console.WriteLine("Introduceti ID-ul produsului pe care doriti sa-l adaugati in cos:");
            int productId = int.Parse(Console.ReadLine());

            Product selectedProduct = _productQueryService.FindProductById(productId);
            if (selectedProduct != null && selectedProduct.Stock > 0)
            {
                cos.Add(productId);
                Console.WriteLine($"Produsul {selectedProduct.Name} a fost adaugat in cos.");
            }
            else
            {
                Console.WriteLine("Produs indisponibil sau ID incorect.");
            }
        }

        public void PlaceOrder()
        {
            if (cos.Count == 0)
            {
                Console.WriteLine("Cosul este gol. Adaugati produse inainte de a plasa comanda.");
                return;
            }

            int totalAmount = 0;
            string shippingAddress = _customer.BillingAddress;

            int newOrderId = _ordersQueryService.GenerateId();
            Order newOrder = new Order(newOrderId, _customer.Id, 0, shippingAddress);

            foreach (int productId in cos)
            {
                Product product = _productQueryService.FindProductById(productId);
                if (product != null)
                {
                    int orderDetailId = _orderDetailsQueryService.GenerateId();
                    OrderDetail orderDetail = new OrderDetail(orderDetailId, newOrderId, productId, product.Price, 1);

                    _orderDetailsCommandService.AddOrderDetails(orderDetail);

                    totalAmount += product.Price;
                }
            }

            newOrder.Amount = totalAmount;
            _ordersCommandService.AddOrders(newOrder);

            Console.WriteLine("Comanda a fost plasata cu succes.");
            cos.Clear();
        }

        public void EditOrder()
        {
            List<Order> allOrders = _ordersQueryService.GetAllOrders();
            List<Order> customerOrders = new List<Order>();

            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders[i].CustomerId == _customer.Id)
                {
                    customerOrders.Add(allOrders[i]);
                }
            }

            if (customerOrders.Count == 0)
            {
                Console.WriteLine("Nu aveti comenzi de editat.");
                return;
            }

            Console.WriteLine("Comenzile dvs.:");
            for (int i = 0; i < customerOrders.Count; i++)
            {
                Order order = customerOrders[i];
                Console.WriteLine("ID: " + order.Id + ", Total: " + order.Amount + ", Adresa de livrare: " + order.ShippingAddress);
            }

            Console.Write("Introduceti ID-ul comenzii pe care doriti sa o editati: ");
            int orderId = int.Parse(Console.ReadLine());

            Order selectedOrder = null;
            for (int i = 0; i < customerOrders.Count; i++)
            {
                if (customerOrders[i].Id == orderId)
                {
                    selectedOrder = customerOrders[i];
                    break;
                }
            }

            if (selectedOrder == null)
            {
                Console.WriteLine("ID-ul comenzii nu este valid.");
                return;
            }

            Console.WriteLine("Editare comanda - ID: " + selectedOrder.Id);

            Console.Write("Adresa de livrare actuala (" + selectedOrder.ShippingAddress + "): ");
            string newShippingAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(newShippingAddress))
            {
                selectedOrder.ShippingAddress = newShippingAddress;
            }

            _ordersCommandService.UpdateOrders(orderId, selectedOrder);

            Console.WriteLine("Comanda a fost actualizata cu succes.");
        }



    }
}
