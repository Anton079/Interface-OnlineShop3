using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Customers.Models;
using System;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.Products.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.OrderDetails.Repository;
using Interface_OnlineShop3.Customers.Repository;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.Customers.Service;
using Interface_OnlineShop3.playground;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using System.Security.Cryptography.X509Certificates;
using Interface_OnlineShop3.Admins.Repository;
using Interface_OnlineShop3.Admins.Service;
using Interface_OnlineShop3.Admins.Models;


namespace Interface_OnlineShop3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            CustomerRepository customerRepository = new CustomerRepository();
            ProductRepository productRepository = new ProductRepository();
            AdminRepository adminRepository = new AdminRepository();

            OrdersQueryService ordersQueryService = new OrdersQueryService(ordersRepository);
            OrdersCommandService ordersCommandService = new OrdersCommandService(ordersRepository, orderDetailsRepository, productRepository);

            OrderDetailsQueryService orderDetailsQueryService = new OrderDetailsQueryService(orderDetailsRepository);
            OrderDetailsCommandService orderDetailsCommandService = new OrderDetailsCommandService(orderDetailsRepository);

            CustomerQueryService customerQueryService = new CustomerQueryService(customerRepository);
            CustomerCommandService customerCommandService = new CustomerCommandService(customerRepository);

            AdminQueryService adminQueryService = new AdminQueryService(adminRepository);

            ProductQueryService productQueryService = new ProductQueryService(productRepository);
            ProductComandService productComandService = new ProductComandService(productRepository);
            Cos cos = new Cos(productQueryService);


            //ViewLogin viewLogin = new ViewLogin(productComandService,productRepository,adminQueryService,adminRepository, 
            //    customerQueryService,customerRepository, ordersCommandService,  orderDetailsCommandService,productQueryService,
            //    ordersQueryService,orderDetailsQueryService,cos);

            //viewLogin.Play();


          

            ordersCommandService.RemoveOrders(1);





        }
    }
}
