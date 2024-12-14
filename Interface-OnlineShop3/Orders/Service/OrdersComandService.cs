using Interface_OnlineShop3.Customers.Models;
using Interface_OnlineShop3.Customers.Repository;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Repository;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Repository;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Service
{
    public class OrdersCommandService : IOrdersCommandService
    {
        private IOrdersRepository _ordersRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IProductRepository _productRepository;
     

        public OrdersCommandService(IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, IProductRepository productRepository)
        {
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
        }

        public Order AddOrders(Order orders)
        {
            if (orders != null)
            {
                _ordersRepository.AddOrder(orders);
                return orders;
            }
            return null;
        }

        public int RemoveOrders(int id)
        {
            if (_ordersRepository.FindById(id) != null)
            {
                _ordersRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public Order UpdateOrders(int id, Order orders)
        {
            if (orders != null)
            {
                _ordersRepository.UpdateOrders(id, orders);
                return orders;
            }
            return null;
        }

        public Order PlaceOrder(List<OrderDetailsDto> detailsDtos, int customerId, string customerAddress)
        {
            foreach(OrderDetailsDto orderDto in detailsDtos)
            {
                // Creeaza comanda
                Order newOrder = new Order(orderDto.ProductId, customerId, 0, customerAddress);
                _ordersRepository.AddOrder(newOrder);

                // Gaseste produsul dupa nume
                Product productPrice = _productRepository.FindByName(orderDto.ProductName);
                if (productPrice == null)
                {
                    return null;
                }
                int prdPrice = productPrice.Price;
                int idNewGen = newOrder.Id + 10;

                // Creeaza detaliile comenzii
                OrderDetail newOrderDetail = new OrderDetail(idNewGen, newOrder.Id, orderDto.ProductId, prdPrice, orderDto.Quantity);
                _orderDetailsRepository.AddOrderDetail(newOrderDetail);

                // Actualizeaza suma comenzii
                newOrder.Amount += prdPrice * orderDto.Quantity;

                Console.WriteLine($"Comanda cu datele Id OrderDetail:{idNewGen}, Id Order{newOrder.Id}, Id Product:{orderDto.ProductId}, Product Price:{productPrice}, Quantity:{orderDto.Quantity} a fost plasata");

                // Returnează comanda imediat ce a fost creată
                return newOrder;
            }

            return null;// Daca lista este goala, returneaza null
        }


    }
}
