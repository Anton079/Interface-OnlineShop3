using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using System;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.Products.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.OrderDetails.Repository;
using Interface_OnlineShop3.Orders.Service;
using Interface_OnlineShop3.playground;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using System.Security.Cryptography.X509Certificates;
using Interface_OnlineShop3.Users.Repository;
using Interface_OnlineShop3.Users.Service;


namespace Interface_OnlineShop3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            ProductRepository productRepository = new ProductRepository();
            UserRepository userRepository = new UserRepository(); 

            OrdersQueryService ordersQueryService = new OrdersQueryService(ordersRepository);
            OrdersCommandService ordersCommandService = new OrdersCommandService(ordersRepository, orderDetailsRepository, productRepository);
            OrderDetailsQueryService orderDetailsQueryService = new OrderDetailsQueryService(orderDetailsRepository);
            OrderDetailsCommandService orderDetailsCommandService = new OrderDetailsCommandService(orderDetailsRepository);
            ProductQueryService productQueryService = new ProductQueryService(productRepository);
            ProductComandService productCommandService = new ProductComandService(productRepository);
            UserQueryService userQueryService = new UserQueryService(userRepository); 
            UserCommandService userCommandService = new UserCommandService(userRepository); 
            Cos cos = new Cos(productQueryService);

            ViewLogin viewLogin = new ViewLogin(
                userRepository,          
                userQueryService,        
                userCommandService,      
                productCommandService,
                productRepository,
                ordersCommandService,
                orderDetailsCommandService,
                productQueryService,
                ordersQueryService,
                orderDetailsQueryService,
                cos);


            viewLogin.Play();




            //ordersCommandService.RemoveOrders(1);



        }
    }
}
