using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Service;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Customers.Models;
using System;
using Interface_OnlineShop.Customers.Repository;
using Interface_OnlineShop.Customers.Service;
using Interface_OnlineShop.OrderDetails.Repository;
using Interface_OnlineShop.OrderDetails.Service;
using Interface_OnlineShop.Orders.Repository;
using Interface_OnlineShop.Orders.Service;
using Interface_OnlineShop.Products.Service;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.Products.Service;
using Interface_OnlineShop3.Products.Models;
using System.Runtime.CompilerServices;

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

            OrdersQueryService ordersQueryService = new OrdersQueryService(ordersRepository);
            OrdersCommandService ordersCommandService = new OrdersCommandService(ordersRepository);

            OrderDetailsQueryService orderDetailsQueryService = new OrderDetailsQueryService(orderDetailsRepository);
            OrderDetailsCommandService orderDetailsCommandService = new OrderDetailsCommandService(orderDetailsRepository);

            CustomerQueryService customerQueryService = new CustomerQueryService(customerRepository);
            CustomerCommandService customerCommandService = new CustomerCommandService(customerRepository);

            ProductQueryService productQueryService = new ProductQueryService(productRepository);
            ProductComandService productComandService = new ProductComandService(productRepository);





            //View view = new View(ordersQueryService, ordersCommandService, orderDetailsQueryService, orderDetailsCommandService, customerQueryService, customerCommandService, productQueryService, productComandService);

            //view.Play();





            Product pro = new Product(6, "pepene",10,"rosu",101012024,50);
            Product pro2 = new Product(7, "Napoitana", 11, "dulce", 10101, 34);


            //productRepository.Add(pro);

            //productRepository.Remove(6);

            //productRepository.UpdateProducts(3,pro2);

            //productComandService.AddProduct(pro2);

            //productComandService.RemoveProduct(7);





            Customer customer = new Customer(4, "ionut@gmail.com", "raul", "raulmarius", "buc");
            Customer updateCustomer = new Customer(6, "Raulentiu@gmail.com", "miha", "manolache", "Pitesti");

            //customerRepository.Add(customer);

            //customerRepository.UpdateCustomer(2, updateCustomer);

            //customerRepository.Remove(3);

            //customerCommandService.UpdateCustomer(1, updateCustomer);

            //customerCommandService.AddCustomer(updateCustomer);

            //customerCommandService.RemoveCustomer(6);





            Order order = new Order(1, 1, 10,"Pitesti");
            Order orderNew = new Order(2, 2, 20, "Buc");

            //ordersRepository.AddOrder(order);

            //ordersCommandService.AddOrders(order);

            //ordersCommandService.UpdateOrders(orderNew);         ??????????




            //string rezultat = productRepository.ToSaveAll();

            //Console.WriteLine(rezultat);






        }
    }
}
