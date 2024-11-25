using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Service;
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

        private Customer customer = new Customer(1, "anton@gmail.com", "raul", "manolache", "Cluj");

        public MainView(IOrdersCommandService ordersCommandService, IOrderDetailsCommandService orderDetailsCommandService, IProductQueryService productQueryService, IOrdersQueryService ordersQueryService, IOrderDetailsQueryService orderDetailsQueryService)
        {
            _ordersCommandService = ordersCommandService;
            _orderDetailsCommandService = orderDetailsCommandService;
            _productQueryService = productQueryService;
            _ordersQueryService = ordersQueryService;
            _orderDetailsQueryService = orderDetailsQueryService;
        }

        public void MeniuView()
        {
            Console.WriteLine("1. Afisare produse");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }

        public void Play()
        {
            bool running = true;
            while (running)
            {
                MeniuView();
                string alegere = Console.ReadLine();

                switch(alegere)
                {
                    case "1":
                        AfisareProduse();
                        break;
                }
            }
        }

        public void AfisareProduse()
        {
            List<Product> products = _productQueryService.getAll();

            foreach(Product product in products)
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Descriptions}");
            }
        }



    }
}
