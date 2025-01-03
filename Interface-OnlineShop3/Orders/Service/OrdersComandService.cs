﻿using Interface_OnlineShop3.Customers.Models;
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

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(1, 1000000);

            while(_ordersRepository.FindById(id) != null)
            {
                id = rand.Next(1, 10000000);
            }
            return id;
        }

        public void PlaceOrder(IList<OrderDetailsDto> detailsDtos, int customerId, string customerAddress)
        {
            int orderId = _ordersRepository.GenerateId();

            Order order = new Order(orderId, customerId, 0, customerAddress);

            int totalAmount = 0;

            foreach (OrderDetailsDto dto in detailsDtos)
            {
                
                int orderDetailId=_orderDetailsRepository.GenerateId();

                OrderDetail orderDetail = new OrderDetail(orderDetailId, orderId, dto.ProductId, dto.Price, dto.Quantity);

                _orderDetailsRepository.AddOrderDetail(orderDetail);

                totalAmount += dto.Price * dto.Quantity;          
            }
  

            order.Amount = totalAmount;
            _ordersRepository.AddOrder(order);
            _orderDetailsRepository.SaveData();
            _ordersRepository.SaveData();
            _productRepository.SaveData();
        }


    }
}
